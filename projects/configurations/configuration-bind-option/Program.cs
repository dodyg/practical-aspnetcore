var builder = WebApplication.CreateBuilder();

ConfigurationManager configuration = builder.Configuration;

var app = builder.Build();

app.Run(context =>
{
    var positionOptions = new PositionOptions();
    configuration.GetSection(PositionOptions.Position).Bind(positionOptions);

    return context.Response.WriteAsync($"Title: {positionOptions.Title} \n" +
                       $"Name: {positionOptions.Name}");
});

app.Run();


public class PositionOptions
{
    public const string Position = "Position";

    public string Title { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
}

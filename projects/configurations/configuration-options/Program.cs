using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder();

builder.Services.Configure<ApplicationOptions>(o =>
{
    o.Name = "Options Sample";
    o.MaximumLimit = 10;
});

var app = builder.Build();
app.Run(context =>
{
    var options = context.RequestServices.GetService<IOptions<ApplicationOptions>>();

    return context.Response.WriteAsync($"Settings Name : {options.Value.Name}  - Maximum limit : {options.Value.MaximumLimit}");
});

app.Run();

public class ApplicationOptions
{
    public string Name { get; set; }

    public int MaximumLimit { get; set; }
}

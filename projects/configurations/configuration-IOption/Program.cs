using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MyOptions>(builder.Configuration.GetSection("MyOptions"));

var app = builder.Build();

app.MapGet("/", (IOptions<MyOptions> optionsDelegate) => 
    optionsDelegate.Value.Option2
);

app.Run();


public class MyOptions
{
    public string Option1 { get; set; } = string.Empty;
    public int Option2 { get; set; }
}


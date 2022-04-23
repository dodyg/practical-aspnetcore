using Microsoft.Extensions.Hosting.WindowsServices;

// https://stackoverflow.com/questions/69909593/asp-net-6-custom-webapplicationfactory-throws-exception
var options = new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default,
    WebRootPath = "wwwroot",
    ApplicationName = typeof(Program).Assembly.FullName
};

var builder = WebApplication.CreateBuilder(options);
builder.Host.UseWindowsService();
builder.WebHost.UseUrls("http://localhost:5300");
var app = builder.Build();

app.Run(async context =>
{
    await context.Response.WriteAsync($"This is hello world running from a Windows Service");
});

app.Run();

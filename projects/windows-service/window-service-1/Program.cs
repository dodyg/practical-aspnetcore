using Microsoft.Extensions.Hosting.WindowsServices;

var options = new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
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

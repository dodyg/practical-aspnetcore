using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder();
builder.Logging.SetMinimumLevel(LogLevel.Trace);

var app = builder.Build();
app.Logger.LogInformation("System is starting up");

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Log Level Trace is " + app.Logger.IsEnabled(LogLevel.Trace));
});

await app.RunAsync();
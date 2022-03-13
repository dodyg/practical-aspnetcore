using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

var app = WebApplication.Create();
app.Logger.LogInformation("System is starting up");

app.Run(async (context) =>
{
    var logger = context.RequestServices.GetService<ILogger>();
    if (logger is null)
        app.Logger.LogWarning("GetService<ILogger> is null.");
        
    await context.Response.WriteAsync("hello world");
});

await app.RunAsync();
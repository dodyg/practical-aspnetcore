using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

var app = WebApplication.Create();
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Check the log to see when the log messages displayed in response to application lifetime event");
});

app.Lifetime.ApplicationStarted.Register(() => 
{
    app.Logger.LogWarning("Application started");
});

app.Lifetime.ApplicationStopping.Register(() => 
{
    app.Logger.LogWarning("Application stopping");
});


app.Lifetime.ApplicationStopped.Register(() => 
{
    app.Logger.LogWarning("Application stopped");
});

await app.RunAsync();
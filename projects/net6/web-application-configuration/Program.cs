using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

var app = WebApplication.Create();
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Check the configuration information on your console log. This configuration information should never be transmitted because they expose sensitive information.");
    app.Logger.LogInformation((app.Configuration as IConfigurationRoot).GetDebugView());
});

await app.RunAsync();
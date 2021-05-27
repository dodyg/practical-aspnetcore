using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

var app = WebApplication.Create();
app.Run(async (context) =>
{
    await context.Response.WriteAsync((app.Configuration as IConfigurationRoot).GetDebugView());
});

await app.RunAsync();
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

WebApplication app = WebApplication.Create();
app.Run(async context =>
{
    // Duplicate the code below and write more messages. Save and refresh your browser to see the result.
    await context.Response.WriteAsync("Hello world .NET 6. Make sure you run this app using 'dotnet watch run'.");
});

await app.RunAsync();
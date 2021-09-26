using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var options = new WebApplicationOptions
{
    EnvironmentName = Environments.Development
};

var app = WebApplication.Create(args);

app.Run(async (context) =>
{
    await context.Response.WriteAsync($"Environment {options.EnvironmentName}");
});

await app.RunAsync();
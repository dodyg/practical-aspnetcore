using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var options = new WebApplicationOptions
{
    EnvironmentName = Environments.Development
};

var builder = WebApplication.CreateBuilder(options);
var app = builder.Build();

app.Run(async (context) =>
{
    await context.Response.WriteAsync($"Environment {options.EnvironmentName}");
});

await app.RunAsync();
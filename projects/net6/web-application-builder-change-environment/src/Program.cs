using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var options = new WebApplicationOptions
{
    EnvironmentName = Environments.Development
};

WebApplicationBuilder builder = WebApplication.CreateBuilder(options);

var app = builder.Build();
app.Run(async context =>
{
    await context.Response.WriteAsync(@"Environment name " + app.Environment.EnvironmentName);
});

app.Run();
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
await app.RunAsync();

public class HomeController
{
    [HttpGet("/")]
    public string Index() => "Hello World";
}
using Microsoft.OpenApi.Models;
using Minimal.Api.Pokedex;
using Minimal.Api.Pokedex.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo()
{
    Description = "Pokedex API implemetation using .NET 6 Minimal API",
    Title = "Pokedex API",
    Version = "v1", 
    Contact = new OpenApiContact()
    {
        Name = "Lohith GN",
        Url = new Uri("https://github.com/lohithgn")
    }
}));
builder.Services.AddPokedexApi();
var app = builder.Build();

app.UseStaticFiles();
app.UseSwagger();
app.MapPokedexApiRoutes();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokedex Api v1");
    c.RoutePrefix = string.Empty;
});

app.Run();

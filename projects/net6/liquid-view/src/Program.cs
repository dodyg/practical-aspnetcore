var builder = WebApplication.CreateBuilder();
builder.Services.AddFluid();

var app = builder.Build();

app.MapGet("/", ()=> Results.Extensions.View("Index"));

app.Run();
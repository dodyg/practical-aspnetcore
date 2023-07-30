using Microsoft.AspNetCore.Components.Endpoints;

var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorComponents();
var app = builder.Build();

app.MapGet("/", () =>
{
    return new RazorComponentResult<RazorComponentTwo.Pages.Greetings>(new Dictionary<string, object> { ["Message"] = "Hello World " + DateTime.Now.ToString() });
});

app.Run();



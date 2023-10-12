using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorComponents();
var app = builder.Build();

app.MapGet("/", () =>
{
    return new RazorComponentResult<RazorComponentThree.Pages.Greetings>(new { Message = "Hello World too", Date = DateOnly.FromDateTime(DateTime.Now) });
});

app.Run();



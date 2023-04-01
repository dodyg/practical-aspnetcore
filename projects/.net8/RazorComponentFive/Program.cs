using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components.Endpoints;

var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorComponents();
builder.Services.AddMvc();
var app = builder.Build();

app.MapControllers();

app.Run();

public class HomeController : Controller
{
    [Route("/")]
    public IResult Index() => new RazorComponentResult<RazorComponentFive.Pages.Greetings>(new { Message = "Hello World too", Date = DateOnly.FromDateTime(DateTime.Now) });
}


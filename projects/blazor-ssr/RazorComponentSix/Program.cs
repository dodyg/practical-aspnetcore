using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorComponents();
builder.Services.AddMvc();
var app = builder.Build();

app.MapControllers();

app.Run();

public class HomeController : Controller
{
    [Route("/")]
    public IResult Index() => new RazorComponentResult<RazorComponentSix.Pages.Greetings>(new { Message = "Hello World too", Date = DateOnly.FromDateTime(DateTime.Now) });
}


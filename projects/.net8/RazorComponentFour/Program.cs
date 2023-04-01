using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddMvc();
builder.Services.AddRazorComponents();
var app = builder.Build();

app.MapControllers();
app.Run();


public class HomeController : Controller 
{
    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }
}


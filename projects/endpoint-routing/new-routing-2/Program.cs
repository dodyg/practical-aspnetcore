using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapControllers();
app.Run();

[Route("")]
public class HomeController : Controller
{
    public IActionResult Index() => Content("Hello World. Razor Pages won't work in this sample.");
}

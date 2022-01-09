using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();

[Route("")]
public class HomeController : Controller
{
    public IActionResult Index() => Content("Using services.AddControllers to provide MVC without Views supports. Perfect for Web APIs.");
}

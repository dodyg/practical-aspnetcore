using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapDefaultControllerRoute();
app.Run();

public class HomeController : Controller
{
    public ActionResult Index() =>
        ViewComponent("HelloWorld", new { message = "Hello World", times = 10 });
}

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapDefaultControllerRoute();
app.Run();


public class HomeController : Controller
{
    public ActionResult Index()
    {
        return new ContentResult
        {
            Content = "<html><body><b>Hello World</b></body></html>",
            ContentType = "text/html"
        };
    }
}

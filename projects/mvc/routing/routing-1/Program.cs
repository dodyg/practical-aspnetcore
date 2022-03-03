using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapControllerRoute(
    name: "home",
    pattern: "/",
    defaults: new { controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}",
    defaults: new { controller = "Home", action = "Index" });

app.Run();

public class HomeController : Controller
{
    public ActionResult Index()
    {
        return new ContentResult
        {
            Content = @"
                <html><body>
                <b>Hello World</b>
                <br/><br/>
                The following links call the same controller and action.
                <ul>
                    <li><a href=""/"">/</a></li>
                    <li><a href=""/home"">/home</a></li>
                    <li><a href=""/home/index"">/home/index</a></li>
                </ul>
                </body></html>",
            ContentType = "text/html"
        };
    }
}



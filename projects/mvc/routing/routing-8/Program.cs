using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapControllers();
app.Run();


[Route("[controller]/")]
public class HomeController : Controller
{
    [HttpGet("/")]
    [HttpGet("")]
    public ActionResult Index()
    {
        return new ContentResult
        {
            Content = @"
                <html><body>
                <h1>[controller] and [action] replacement tokens examples</h1>
                <ul>
                    <li><a href=""/"">/</a></li>
                    <li><a href=""/home/"">/home/</a></li>
                    <li><a href=""/home/about"">/home/about</a></li>
                    <li><a href=""/about"">/about</a></li>
                    <li><a href=""/2/about2"">/2/about2</a></li>
                </ul>
                </body></html>",
            ContentType = "text/html"
        };
    }

    [HttpGet("[action]")]
    public ActionResult About()
    {
        return new ContentResult
        {
            Content = @"
                <html><body>
                <b>About Page using replacement token [action]</b
                </body></html>",
            ContentType = "text/html"
        };
    }

    [HttpGet("/about")]
    [HttpGet("/2/[action]")]
    public ActionResult About2()
    {
        return new ContentResult
        {
            Content = @"
                <html><body>
                <b>About Page 2</b
                </body></html>",
            ContentType = "text/html"
        };
    }
}



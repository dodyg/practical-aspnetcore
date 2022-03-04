using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapControllers();

app.Run();

public class HomeController : Controller
{
    [Route("")]
    [Route("Home")]
    [Route("Home/Index")]
    public ActionResult Index()
    {
        return new ContentResult
        {
            Content = @"
                <html><body>
                <h1>[HttpGet] and [HttpPost]</h1>
                <ul>
                    <li><a href=""/about"">/about</a></li>
                </ul>

                <form action=""about"" method=""post"">
                    <button>Post About</button>
                </form>
                </body></html>",
            ContentType = "text/html"
        };
    }

    [HttpGet("About")]
    public ActionResult About()
    {
        return new ContentResult
        {
            Content = @"
                <html><body>
                <b>About Page - GET</b
                </body></html>",
            ContentType = "text/html"
        };
    }

    [HttpPost("About")]
    public ActionResult About2()
    {
        return new ContentResult
        {
            Content = @"
                <html><body>
                <b>About Page - POST</b
                </body></html>",
            ContentType = "text/html"
        };
    }
}

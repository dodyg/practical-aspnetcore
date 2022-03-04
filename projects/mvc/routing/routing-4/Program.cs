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
                <b>Hello World</b>
                <br/><br/>
                <ul>
                    <li><a href=""/"">/</a></li>
                    <li><a href=""/home"">/home</a></li>
                    <li><a href=""/home/index"">/home/index</a></li>
                    <li><a href=""/about"">/about</a></li>
                </ul>
                </body></html>",
            ContentType = "text/html"
        };
    }

    [Route("About")]
    public ActionResult About()
    {
        return new ContentResult
        {
            Content = @"
                <html><body>
                <b>About Page</b>
                <br/><br/>
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



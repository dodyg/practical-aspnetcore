using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapControllerRoute(
    "About",
    "{id}/About",
    defaults: new { controller = "Home", Action = "About" }
);

app.Run();

public class NumberAttribute : Attribute, IActionConstraint
{
    private readonly int _number;

    public NumberAttribute(int number)
    {
        _number = number;
    }

    public int Order
    {
        get
        {
            return 0;
        }
    }

    public bool Accept(ActionConstraintContext context)
    {
        return Convert.ToInt32(context.RouteContext.RouteData.Values["id"]) == _number;
    }
}

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
                <h1>Custom Routing Constraint Attribute</h1>
                <ul>
                    <li><a href=""/"">/</a></li>
                    <li><a href=""/1/about"">/1/about</a></li>
                    <li><a href=""/2/about"">/2/about</a></li>
                    <li><a href=""/3/about"">/3/about</a></li>
                </ul>
                </body></html>",
            ContentType = "text/html"
        };
    }
}

namespace PracticalAspNetCore.Route1
{
    public class HomeController : Controller
    {
        [Number(1)]
        public ActionResult About()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <b>About Page 1</b
                </body></html>",
                ContentType = "text/html"
            };
        }
    }
}

namespace PracticalAspNetCore.Route2
{
    public class HomeController : Controller
    {
        [Number(2)]
        public ActionResult About()
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
}

namespace PracticalAspNetCore.Route3
{
    public class HomeController : Controller
    {
        [Number(3)]
        public ActionResult About()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <b>About Page 3</b
                </body></html>",
                ContentType = "text/html"
            };
        }
    }
}
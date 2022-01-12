using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<NumberTransformer>();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapControllers();
app.MapDynamicControllerRoute<NumberTransformer>("{number}");

app.Run();

public class NumberTransformer : DynamicRouteValueTransformer
{
    public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
    {
        if (!values.ContainsKey("number"))
            return new ValueTask<RouteValueDictionary>(values);

        values["controller"] = "Home";

        var action = values["number"] switch
        {
            "1" => "one",
            "2" => "two",
            "3" => "three",
            _ => "undefined"
        };

        values["action"] = action;

        return new ValueTask<RouteValueDictionary>(values);
    }
}

public class HomeController : Controller
{
    [Route("")]
    public ActionResult Index()
    {
        return Content(@"<html><body>
        <ul>
            <li><a href=""/1"">/1</a></li>
            <li><a href=""/2"">/2</a></li>
            <li><a href=""/3"">/3</a></li>
            <li><a href=""/4"">/4</a></li>
        </ul>
        </body></html>
        ", "text/html");
    }

    public ActionResult One()
    {
        return Content(@"One");
    }

    public ActionResult Two()
    {
        return Content(@"Two");
    }

    public ActionResult Three()
    {
        return Content(@"Three");
    }

    public ActionResult Undefined()
    {
        return Content("Undefined");
    }
}

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();
builder.Services.AddApiVersioning(o => o.ReportApiVersions = true);

var app = builder.Build();
app.MapDefaultControllerRoute();
app.Run();

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class HelloWorldController : ControllerBase
{
    [HttpGet]
    public IActionResult Get(ApiVersion apiVersion) => Ok(new { Controller = GetType().Name, Version = apiVersion.ToString() });
}

public class HomeController : Controller
{
    public ActionResult Index()
    {
        return new ContentResult
        {
            Content = @"
                <html>
                    <body>
                    <ul>
                        <li><a href=""/api/v1/helloWorld"">Click here for Version 1</a></li>
                        <li><a href=""/api/v2/helloWorld"">Click here for Version 2</a></li>
                    </ul>
                    </body>
                </html>",
            ContentType = "text/html"
        };
    }
}

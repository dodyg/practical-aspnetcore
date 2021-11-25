using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder();

builder.Services.AddHealthChecks();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapHealthChecks("/IsUp", new HealthCheckOptions
{
    ResponseWriter = async (context, health) =>
    {
        await context.Response.WriteAsync("Mucho Bien");
    }
});

app.MapDefaultControllerRoute();

app.Run();

public class HomeController : Controller
{
    public ActionResult Index()
    {
        return new ContentResult
        {
            Content = @"
                <html><body>
                <h1>Health Check - custom message</h1>
                The health check service checks on this url <a href=""/isup"">/isup</a>. It will return  `Mucho Bien` thanks to the customized health message.
                </body></html> ",
            ContentType = "text/html"
        };
    }
}

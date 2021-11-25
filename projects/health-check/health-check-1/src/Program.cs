using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();

builder.Services.AddHealthChecks();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapHealthChecks("/WhatsUp");
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
                <h1>Health Check</h1>
                The health check service checks on this url <a href=""/WhatsUp"">/WhatsUp</a>. 
                </body></html> ",
            ContentType = "text/html"
        };
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapDefaultControllerRoute();
app.Run();

public class HomeController : Controller
{
    IWebHostEnvironment _env;

    public HomeController(IWebHostEnvironment env)
    {
        _env = env;
    }

    public ActionResult Index()
    {
        return new ContentResult
        {
            Content = "<html><body><h1>Download File</h1>Download a copy of  <a href=\"/home/hegel\">Hegel (pdf)</a></body></html>",
            ContentType = "text/html"
        };
    }

    public PhysicalFileResult Hegel()
    {
        var pathToIdeas = System.IO.Path.Combine(_env.WebRootPath, "hegel.pdf");

        return new PhysicalFileResult(pathToIdeas, "application/pdf");
    }
}

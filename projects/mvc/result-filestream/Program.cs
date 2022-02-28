using Microsoft.AspNetCore.Mvc;

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

    public FileStreamResult Hegel()
    {
        var pathToIdeas = System.IO.Path.Combine(_env.WebRootPath, "hegel.pdf");

        //This is a contrite example to demonstrate returning a stream. If you have a physical file on disk, just use PhySicalFileResult that takes a path. 
        return new FileStreamResult(System.IO.File.OpenRead(pathToIdeas), "application/pdf")
        {
            FileDownloadName = "hegel.pdf"
        };
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder();
builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();

var supportedCultures = new List<CultureInfo>
{
    new CultureInfo("fr-FR")
};

var options = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("fr-FR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

app.UseRequestLocalization(options);
app.MapDefaultControllerRoute();
app.Run();

namespace MvcLocalization
{
    // Leave this class empty
    public class Global
    {

    }
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
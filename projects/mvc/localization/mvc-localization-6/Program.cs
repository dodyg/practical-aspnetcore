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
    new CultureInfo("fr-FR"),
    new CultureInfo("en-US")
};

var options = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("fr-FR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

/*
    By default you have the following providers. 
    - QueryStringRequestCultureProvider
    - CookieRequestCultureProvider
    - AcceptLanguageHeaderRequestCultureProvider

    AcceptLanguageHeaderRequestCultureProvider is the one that check for client's language preference. We don't want this. 
    */

options.RequestCultureProviders.Clear();
options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider());
options.RequestCultureProviders.Add(new CookieRequestCultureProvider());

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
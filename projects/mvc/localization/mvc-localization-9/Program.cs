using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder();
builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();

var supportedCultures = new List<CultureInfo>
{
    new CultureInfo("fr-FR"),
    new CultureInfo("en-US"),
    new CultureInfo("en"),
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
    */

options.RequestCultureProviders.Clear();
options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());

app.UseRequestLocalization(options);
app.MapDefaultControllerRoute();
app.Run();

namespace MvcLocalization
{
    // Leave this class empty
    public class Global
    {
    }

    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        IStringLocalizer<Global> _global;

        public ApiController(IStringLocalizer<Global> global)
        {
            _global = global;
        }

        [HttpGet("{word}")]
        public ActionResult Get(string word)
        {
            return Ok(new { Greeting = _global[word].ToString() });
        }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
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
        readonly IStringLocalizer<Global> _local;

        public HomeController(IStringLocalizer<Global> local)
        {
            _local = local;
        }
        public ActionResult Index()
        {
            var culture = this.HttpContext.Features.Get<IRequestCultureFeature>();

            return new ContentResult
            {
                Content = $@"<html><body>
                    <h1>MVC Shared Resources - Home</h1>
                    <p>
                        <a href=""/about/index"">About</a>
                    </p>
                    <b>Culture requested</b> {culture.RequestCulture.Culture} <br/>
                    <b>UI Culture requested</b> {culture.RequestCulture.UICulture} <br/>
                    Text: {_local["Hello"]}<br/>
                    Text: {_local["Goodbye"]}</body></html>",
                ContentType = "text/html"
            };
        }
    }

    public class AboutController : Controller
    {
        readonly IStringLocalizer<Global> _local;

        public AboutController(IStringLocalizer<Global> local)
        {
            _local = local;
        }
        public ActionResult Index()
        {
            var culture = this.HttpContext.Features.Get<IRequestCultureFeature>();

            return new ContentResult
            {
                Content = $@"<html><body>
                    <h1>MVC Shared Resources - About</h1>
                    <p>
                        <a href=""/"">Home</a>
                    </p>
                    <b>Culture requested</b> {culture.RequestCulture.Culture} <br/>
                    <b>UI Culture requested</b> {culture.RequestCulture.UICulture} <br/>
                    Text: {_local["Hello"]}<br/>
                    Text: {_local["Goodbye"]}</body></html>",
                ContentType = "text/html"
            };
        }
    }
}
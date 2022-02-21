using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder();
builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
builder.Services.AddControllersWithViews();
builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
      options.RequestCultureProviders.Clear();

      var supportedCultures = new List<CultureInfo>
      {
          new CultureInfo("fr"),
          new CultureInfo("en-US"),
          new CultureInfo("en"),
      };

      options.DefaultRequestCulture = new RequestCulture("fr");
      options.SupportedCultures = supportedCultures;
      options.SupportedUICultures = supportedCultures;

      options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(context =>
      {
          var defaultCulture = options.DefaultRequestCulture.Culture;

          var segments = context.Request.Path.Value.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

          if (segments.Length > 0)
          {
            foreach (var c in options.SupportedCultures)
            {
              if (c.Name.Equals(segments[0], StringComparison.InvariantCulture))
                return Task.FromResult(new ProviderCultureResult(c.Name));
            }
          }

          return Task.FromResult(new ProviderCultureResult(defaultCulture.Name));
        }));
    });

var app = builder.Build();
app.UseStaticFiles();
app.UseRequestLocalization();
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
      [Route("{*url}")]
      public ActionResult Index()
      {
        return View();
      }
    }
}
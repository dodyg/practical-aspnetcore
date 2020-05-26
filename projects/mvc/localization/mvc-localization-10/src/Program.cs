using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace MvcLocalization
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
      services.AddControllersWithViews();

      services.Configure<RequestLocalizationOptions>(options =>
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
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseRouting();
      app.UseStaticFiles();

      app.UseRequestLocalization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
      });
    }
  }

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

  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.UseStartup<Startup>()
            );
  }
}
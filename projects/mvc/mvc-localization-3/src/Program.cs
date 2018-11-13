using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

namespace MvcLocalization
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //These are three services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            services.AddMvcCore().
                SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
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

            app.UseMvc(routes =>
                routes.MapRoute(
                    name: "default_route",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" })
                );
        }
    }

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

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}
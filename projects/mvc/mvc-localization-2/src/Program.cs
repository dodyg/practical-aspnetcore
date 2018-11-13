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

    public class HomeController : Controller
    {
        readonly IStringLocalizer<HomeController> _local;

        public HomeController(IStringLocalizer<HomeController> local)
        {
            _local = local;
        }
        public ActionResult Index()
        {
            var culture = this.HttpContext.Features.Get<IRequestCultureFeature>();

            return new ContentResult
            {
                Content = $@"<html><body>
                <h1>MVC Localization Resource File Naming - Path Naming Convention</h1>
                <p>
                <i><b>Resources are named for the full type name of their class minus the assembly name</b>. For example, a French resource in a project whose main assembly is LocalizationWebsite.Web.dll for the class LocalizationWebsite.Web.Startup would be named Startup.fr.resx. A resource for the class LocalizationWebsite.Web.Controllers.HomeController would be named Controllers.HomeController.fr.resx. If your targeted class's namespace isn't the same as the assembly name you will need the full type name. For example, in the sample project a resource for the type ExtraNamespace.Tools would be named ExtraNamespace.Tools.fr.resx.
                </i>
                <a href=""https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-2.1\"">Doc</a>
                </p>
                <p>
                    In this sample, the assembly name is ""mvc-localization"" however the namespace used is ""MvcLocalization"". Since the namespace differs from the Assembly Name, we use full type path.
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
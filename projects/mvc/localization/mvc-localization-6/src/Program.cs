using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;

namespace MvcLocalization
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
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

    public class HomeController : Controller
    {
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
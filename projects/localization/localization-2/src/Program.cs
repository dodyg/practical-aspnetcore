using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore;

namespace Local 
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are two services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //put the resource files under resources folder
            services.AddLocalization(options => options.ResourcesPath = "resources");
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IStringLocalizerFactory stringLocalizerFactory)
        {
            var local = stringLocalizerFactory.Create("Common", typeof(Program).Assembly.FullName);

            //This section is important otherwise aspnet won't be able to pick up the resource
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

            app.UseRequestLocalization(options);

            //These are the three default services available at Configure
            app.Run(async context =>
            {
                await context.Response.WriteAsync("<h1>QueryStringRequestCultureProvider</h1><p>We are using query string to switch the request culture.</p>");

                var requestCulture = context.Features.Get<IRequestCultureFeature>().RequestCulture;
                if (requestCulture.Culture.TwoLetterISOLanguageName != "fr")
                    await context.Response.WriteAsync($"<a href=\"/?culture=fr-FR&ui-culture=fr-FR\">Switch to French</a><br/>");
                else if (requestCulture.Culture.TwoLetterISOLanguageName != "en")
                    await context.Response.WriteAsync($"<a href=\"/?culture=en-US&ui-culture=en-US\">Switch to English</a><br/>");
                
                await context.Response.WriteAsync($@"
                Request Culture: {requestCulture.Culture} <br/> 
                Localized strings: {local["Hello"]} {local["Goodbye"]} {local["Yes"]} {local["No"]}
                ");
            });
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
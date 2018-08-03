using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

namespace StartupBasic
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //These are three services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddPortableObjectLocalization();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //We are limiting the supported culture here so this sample works in any browser from different culture setting.
            //To make it pick up French or other language, simply change it-IT with something else or add more supported cultures
            //I have added PO file for French and English
            var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("it-IT")
                    };

            var option = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("it-IT"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(option);

            app.Run(context =>
            {
                var fac = context.RequestServices.GetService<IStringLocalizerFactory>();
                var local = fac.Create(string.Empty, string.Empty);

                var requestCulture = context.Features.Get<IRequestCultureFeature>().RequestCulture;
                
                return context.Response.WriteAsync($"Request Culture `{requestCulture.UICulture}` = {local["Hello"]}");
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
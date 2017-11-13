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
using OrchardCore.Localization.PortableObject;

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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration, IStringLocalizerFactory localizer)
        {
            var local = localizer.Create(string.Empty, string.Empty);
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("en"),
                new CultureInfo("it-it"),
                new CultureInfo("it")
            };

           var options = new RequestLocalizationOptions
           {
                DefaultRequestCulture = new RequestCulture("it-it"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
           };

            app.UseRequestLocalization(options);  

            //These are the four default services available at Configure
            app.Run(context =>
            {
                return context.Response.WriteAsync($"{local["greeting"]}");
            });
        }
    }
    
   public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development")
                .Build();
    }
}
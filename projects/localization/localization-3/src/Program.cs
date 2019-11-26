using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Globalization;
using System;
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
                new CultureInfo("en-US"),
                new CultureInfo("en-GB")
            };
            
            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-GB"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(options);

            //These are the three default services available at Configure
            app.Run(async context =>
            {
                await context.Response.WriteAsync("<h1>Culture vs UI Culture</h1><p>UI Culture affects the resource string. Culture affects number formatting, dates, etc.</p>");

                var requestCulture = context.Features.Get<IRequestCultureFeature>().RequestCulture;

                await context.Response.WriteAsync($@"
                <a href=""/?culture=en-US&ui-culture=en-GB"">Culture US - UI Culture GB</a><br/>
                <a href=""/?culture=en-GB&ui-culture=en-US"">Culture GB - UI Culture US</a><br/>
                <br/>
                Request Culture: {requestCulture.Culture} <br/>
                Today's Date (Culture): {DateTime.Now.ToString()}<br/><br/>
                Request UI Culture: {requestCulture.UICulture}<br/> 
                Localized strings (UI Culture): {local["Hello"]} {local["Goodbye"]} {local["Yes"]} {local["No"]}
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
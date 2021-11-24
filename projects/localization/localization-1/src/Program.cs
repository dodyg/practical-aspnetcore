using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore 
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "resources");
        }

        public void Configure(IApplicationBuilder app, IStringLocalizerFactory stringLocalizerFactory)
        {
            var local = stringLocalizerFactory.Create("Common", typeof(Program).Assembly.FullName);

            //This section is important otherwise aspnet won't be able to pick up the resource
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

            app.Run(async context =>
            {
                var requestCulture = context.Features.Get<IRequestCultureFeature>();
                await context.Response.WriteAsync($"{requestCulture.RequestCulture.Culture} - {local["Hello"]} {local["Goodbye"]} {local["Yes"]} {local["No"]}");
            });
        }
    }
    

}
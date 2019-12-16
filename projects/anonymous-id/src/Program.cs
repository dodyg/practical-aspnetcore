using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using ReturnTrue.AspNetCore.Identity.Anonymous;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //These are three services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.UseAnonymousId(new AnonymousIdCookieOptionsBuilder()
                .SetCustomCookieName("Anoymous_Cookie_Tracker")
                //.SetCustomCookieRequireSsl(true) //Uncomment this in the case of usign SSL, such as the default setup of .NET Core 2.1 
                .SetCustomCookieTimeout(120)
            );

            app.Run(async context =>
            {
                IAnonymousIdFeature feature = context.Features.Get<IAnonymousIdFeature>();
                string anonymousId = feature.AnonymousId;

                await context.Response.WriteAsync($"Hello world with anonymous id {anonymousId}");
            });
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
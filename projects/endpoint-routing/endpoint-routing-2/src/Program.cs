using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace LinkGeneratorSample
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env, ILoggerFactory logger)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllersWithViews();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.Use(async (context, next) =>
            {
                var linkGenerator = context.RequestServices.GetService<LinkGenerator>();

                var url = linkGenerator.GetUriByAction(context,
                       controller: "Hello",
                       action: "World"
                   );

                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"Generated Url: {url}");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }

    public class HelloController
    {
        public ActionResult World()
        {
            return null;
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
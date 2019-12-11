using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {

            app.UseRouting();
            app.UseEndpoints(route =>
            {
                route.MapHealthChecks("/IsUp", new HealthCheckOptions
                {
                    ResponseWriter = async (context, health) =>
                    {
                        await context.Response.WriteAsync("Mucho Bien");
                    }
                });

                route.MapDefaultControllerRoute();
            });
        }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <h1>Health Check - custom message</h1>
                The health check service checks on this url <a href=""/isup"">/isup</a>. It will return  `Mucho Bien` thanks to the customized health message.
                </body></html> ",
                ContentType = "text/html"
            };
        }
    }

    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}
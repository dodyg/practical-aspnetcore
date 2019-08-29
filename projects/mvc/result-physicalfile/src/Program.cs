using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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
            services.AddMvcCore().
                SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
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
        IHostingEnvironment _env;

        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }

        public ActionResult Index()
        {
            return new ContentResult
            {
                Content = "<html><body><h1>Download File</h1>Download a copy of  <a href=\"/home/hegel\">Hegel (pdf)</a></body></html>",
                ContentType = "text/html"
            };
        }

        public PhysicalFileResult Hegel()
        {
            var pathToIdeas = System.IO.Path.Combine(_env.WebRootPath, "hegel.pdf");

            return new PhysicalFileResult(pathToIdeas, "application/pdf");
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
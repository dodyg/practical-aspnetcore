using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace MvcRouting
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
            app.UseMvc();
        }
    }

    public class HomeController : Controller
    {
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public ActionResult Index()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <h1>[HttpGet] and [HttpPost]</h1>
                <ul>
                    <li><a href=""/about"">/about</a></li>
                </ul>

                <form action=""about"" method=""post"">
                    <button>Post About</button>
                </form>
                </body></html>",
                ContentType = "text/html"
            };
        }

        [HttpGet("About")]
        public ActionResult About()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <b>About Page - GET</b
                </body></html>",
                ContentType = "text/html"
            };
        }

        [HttpPost("About")]
        public ActionResult About2()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <b>About Page - POST</b
                </body></html>",
                ContentType = "text/html"
            };
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
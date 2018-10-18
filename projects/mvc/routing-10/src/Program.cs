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


    [Route("[controller]/")]
    public class HomeController : Controller
    {
        [HttpGet("/")]
        [HttpGet("")]
        public ActionResult Index()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <h1>[controller] and [action] replacement tokens examples</h1>
                <ul>
                    <li><a href=""/"">/</a></li>
                    <li><a href=""/home/"">/home/</a></li>
                    <li><a href=""/home/about"">/home/about</a></li>
                    <li><a href=""/about"">/about</a></li>
                    <li><a href=""/2/about2"">/2/about2</a></li>
                </ul>
                </body></html>",
                ContentType = "text/html"
            };
        }

        [HttpGet("[action]")]
        public ActionResult About()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <b>About Page using replacement token [action]</b
                </body></html>",
                ContentType = "text/html"
            };
        }

        [HttpGet("/about")]
        [HttpGet("/2/[action]")]
        public ActionResult About2()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <b>About Page 2</b
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
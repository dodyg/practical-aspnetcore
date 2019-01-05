using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;

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
            services.AddMvc().
                SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.UseMvcWithDefaultRoute();
        }
    }

    public class ZeLastController : Controller
    {
        public ActionResult RandomPage()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <h1>ZeLast/RandomPage</h1>
                </body></html>",
                ContentType = "text/html"
            };
        }
    }

    [Route("special-section")]
    public class SpecialController
    {
        [Route("snowflake")]
        public ActionResult RandomPage()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <h1>special-section/snowflake</h1>
                </body></html>",
                ContentType = "text/html"
            };
        }

        [Route("/snowflake")]
        public ActionResult RandomPage2()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <h1>snowflake</h1>
                </body></html>",
                ContentType = "text/html"
            };
        }
    }

    public class HomeController : Controller
    {
        public ActionResult RandomPage()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <h1>Home/RandomPage</h1>
                </body></html>",
                ContentType = "text/html"
            };
        }

        public ActionResult Index()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <h1>Home/Index</h1>
                </body></html>",
                ContentType = "text/html"
            };
        }

        [Route("Special/IAm")]
        public ActionResult AttributeRoutingFromController()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <h1>/Special/Iam</h1>
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

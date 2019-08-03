using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddApiVersioning(o => o.ReportApiVersions = true);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.UseMvcWithDefaultRoute();
        }
    }

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(ApiVersion apiVersion) => Ok(new { Controller = GetType().Name, Version = apiVersion.ToString() });
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new ContentResult
            {
                Content = @"
                <html>
                    <body>
                    <ul>
                        <li><a href=""/api/v1/helloWorld"">Click here for Version 1</a></li>
                        <li><a href=""/api/v2/helloWorld"">Click here for Version 2</a></li>
                    </ul>
                    </body>
                </html>",
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
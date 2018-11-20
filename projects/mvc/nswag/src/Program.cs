using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using NSwag.AspNetCore;
using System.Reflection;
using NJsonSchema;

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
            services.AddMvc().
                SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerDocument(settings =>
            {
                settings.Title = "Sample API";
                settings.DefaultEnumHandling = EnumHandling.CamelCaseString;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUi3(settings =>
            {
                settings.TagsSorter = "alpha";
                settings.OperationsSorter = "alpha";
            });

            app.UseMvc();
        }
    }

    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult Index()
        {
            return new ContentResult
            {
                Content = "<html><body><b><a href=\"/swagger\">View API Documentation</a></b></body></html>",
                ContentType = "text/html"
            };
        }
    }

    [Produces("application/json")]
    [Route("api/greeting")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        public class Greeting
        {
            public string Message { get; set; }
        }

        /// <summary>
        /// This is an API to return a "Hello World" message.
        /// </summary>
        /// <response code="200">The "Hello World" text</response>
        [HttpGet]
        public ActionResult<Greeting> Index()
        {
            return new Greeting
            {
                Message = "Hello World"
            };
        }

        [HttpGet("goodbye")]
        public ActionResult<Greeting> Goodbye()
        {
            return new Greeting
            {
                Message = "Hello World"
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
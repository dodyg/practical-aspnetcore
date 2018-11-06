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

namespace LinkGeneratorSample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.Use(async (context, next) =>
            {
                var linkGenerator = context.RequestServices.GetService<LinkGenerator>();

                var url = linkGenerator.GetUriByAction(context,
                       controller: "Hello",
                       action: "World",
                       values: new
                       {
                           name = "One"
                       }
                   );

                var url2 = linkGenerator.GetUriByAction(context,
                       controller: "Hello",
                       action: "Goodbye",
                       values: new
                       {
                           age = 40
                       }
                   );

                var url3 = linkGenerator.GetUriByAction(context,
                       controller: "Hello",
                       action: "CallMe"
                   );

                var url4 = linkGenerator.GetUriByAction(context,
                       controller: "Greet",
                       action: "Index",
                       values: new
                       {
                           isNice = false
                       }
                   );

                var url5 = linkGenerator.GetUriByAction(context,
                       controller: "Wave",
                       action: "Away",
                       values: new
                       {
                           danger = "real danger",
                           ahead = "5 km ahead"
                       }
                   );

                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($@"Generated Url: 
{url}  
{url2}  
{url3} (the route value is optional) 
{url4}  
{url5}");
            });

            app.UseMvc();
        }
    }

    [Route("[controller]")]
    public class HelloController
    {
        [HttpGet("{name}")]
        public ActionResult World(string name) => null;

        [HttpGet("Goodbye/{age:int}")]
        public ActionResult Goodbye(int age) => null;

        [HttpGet("[action]/{byYourName?}")]
        public ActionResult CallMe(string byYourName) => null;
    }

    [Route("Greet/{isNice:bool}")]
    public class GreetController
    {
        public ActionResult Index() => null;
    }

    public class WaveController
    {
        [Route("Wave-Away/{danger:required}/{ahead:required}")]
        public ActionResult Away(string danger, string ahead) => null;
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
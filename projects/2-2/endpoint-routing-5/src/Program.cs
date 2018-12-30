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
    /// This sample is not working. LinkGenerator.GetTemplateByAction has been removed from the final version of ASP.NET CORE 2.2
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

                LinkGenerationTemplate url = linkGenerator.GetTemplateByAction(
                       controller: "Hello",
                       action: "World"
                   );

                LinkGenerationTemplate url2 = linkGenerator.GetTemplateByAction(
                       controller: "Hello",
                       action: "Goodbye"
                   );

                LinkGenerationTemplate url3 = linkGenerator.GetTemplateByAction(
                       controller: "Hello",
                       action: "CallMe"
                   );

                LinkGenerationTemplate url4 = linkGenerator.GetTemplateByAction(
                       controller: "Greet",
                       action: "Index"
                   );

                LinkGenerationTemplate url5 = linkGenerator.GetTemplateByAction(
                       controller: "Wave",
                       action: "Away"
                   );

                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($@"Generated Url: 
{url.GetPath(context, new { name = "hello" })}  
{url2.GetPath(context, new { age = 40 })}  
{url3.GetPath(context, values: null)} 
{url4.GetPath(context, values: new { isNice = true })}  
{url5.GetPath(context, values: new { danger = "real danger", ahead = "5 km ahead" })}");
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
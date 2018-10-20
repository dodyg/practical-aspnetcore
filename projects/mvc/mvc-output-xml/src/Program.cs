using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace MvcOutputXml
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }

    public class Greeting
    {
        public bool Hello { get; set; }
        public bool World { get; set; }
    }

    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            var h = new Greeting
            {
                Hello = true,
                World = true
            };

            Response.ContentType = "text/xml";
            return Ok(h);
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
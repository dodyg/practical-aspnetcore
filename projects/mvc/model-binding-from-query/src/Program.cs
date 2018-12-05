using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace ModelBinding
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
            app.UseMvcWithDefaultRoute();
        }
    }

    public class GreetingParams
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public bool IsAmazing { get; set; }

        public short? Age { get; set; }

        public override string ToString() => $"User Id: {UserId}, Name: {Name}, Is Amazing: {IsAmazing}, Age: {Age}";
    }

    public class HomeController : Controller
    {
        public ActionResult Index([FromQuery] GreetingParams greet)
        {
            return new ContentResult
            {
                Content = $@"<html><body>
                <h1>Class binding with [FromQuery]</h1>
                <p>You can see the difference in behavior between the nullable type and non nullable types here. Age is short? and User Id is int.<p>
                {greet}
                <ul>
                    <li><a href=""/"">/</a></li>
                    <li><a href=""/?name=annie"">/?name=annie</a></li>
                    <li><a href=""/?isamazing=true"">/?isamazing=true</a></li>
                    <li><a href=""/?userid=1"">/?userid=1</a></li>
                    <li><a href=""/?age=33"">/?age=33</a></li>
                    <li><a href=""/?userid=1&name=annie&isamazing=true"">/?userid=1&name=annie&isamazing=true&age=33</a></li>
                </ul>
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
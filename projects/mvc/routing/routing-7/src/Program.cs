using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }


    [Route("[controller]/[action]")]
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
                    <li><a href=""/home/index"">/home/index</a></li>
                    <li><a href=""/home/about"">/home/about</a></li>
                    <li><a href=""/about"">/about</a></li>
                </ul>
                </body></html>",
                ContentType = "text/html"
            };
        }

        public ActionResult About()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <b>About Page</b
                </body></html>",
                ContentType = "text/html"
            };
        }

        [HttpGet("/about")]
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
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}
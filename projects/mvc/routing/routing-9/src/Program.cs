using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
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
                endpoints.MapControllerRoute(
                    "About",
                    "{id}/About",
                    defaults: new { controller = "Home", Action = "About" }
                );
            });
            
        }
    }

    public class NumberAttribute : Attribute, IActionConstraint
    {
        private readonly int _number;

        public NumberAttribute(int number)
        {
            _number = number;
        }

        public int Order
        {
            get
            {
                return 0;
            }
        }

        public bool Accept(ActionConstraintContext context)
        {
            return Convert.ToInt32(context.RouteContext.RouteData.Values["id"]) == _number;
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
                <h1>Custom Routing Constraint Attribute</h1>
                <ul>
                    <li><a href=""/"">/</a></li>
                    <li><a href=""/1/about"">/1/about</a></li>
                    <li><a href=""/2/about"">/2/about</a></li>
                    <li><a href=""/3/about"">/3/about</a></li>
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
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}

namespace PracticalAspNetCore.Route1
{
    public class HomeController : Controller
    {
        [Number(1)]
        public ActionResult About()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <b>About Page 1</b
                </body></html>",
                ContentType = "text/html"
            };
        }
    }
}

namespace PracticalAspNetCore.Route2
{
    public class HomeController : Controller
    {
        [Number(2)]
        public ActionResult About()
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
}

namespace PracticalAspNetCore.Route3
{
    public class HomeController : Controller
    {
        [Number(3)]
        public ActionResult About()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <b>About Page 3</b
                </body></html>",
                ContentType = "text/html"
            };
        }
    }
}
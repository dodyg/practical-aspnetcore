using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldWithReload
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<NumberTransformer>();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllers();
                endpoint.MapDynamicControllerRoute<NumberTransformer>("{number}");
            });
        }
    }

    public class NumberTransformer : DynamicRouteValueTransformer
    {
        public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            if (!values.ContainsKey("number"))
                return new ValueTask<RouteValueDictionary>(values);

            values["controller"] = "Home";

            var action = values["number"] switch
            {
                "1" => "one",
                "2" => "two",
                "3" => "three",
                _ => "undefined"
            };

            values["action"] = action;

            return new ValueTask<RouteValueDictionary>(values);
        }
    }

    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return Content(@"<html><body>
            <ul>
                <li><a href=""/1"">/1</a></li>
                <li><a href=""/2"">/2</a></li>
                <li><a href=""/3"">/3</a></li>
                <li><a href=""/4"">/4</a></li>
            </ul>
            </body></html>
            ", "text/html");
        }

        public ActionResult One()
        {
            return Content(@"One");
        }

        public ActionResult Two()
        {
            return Content(@"Two");
        }

        public ActionResult Three()
        {
            return Content(@"Three");
        }

        public ActionResult Undefined()
        {
            return Content("Undefined");
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
                {
                    webBuilder.UseStartup<Startup>().
                    UseEnvironment(Environments.Development);
                });
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HelloWorldWithReload
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<NumberTransformer>();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapRazorPages();

                endpoint.MapDynamicPageRoute<NumberTransformer>("{number}");
            });
        }
    }

    public class NumberTransformer : DynamicRouteValueTransformer
    {
        public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            if (!values.ContainsKey("number"))
                return new ValueTask<RouteValueDictionary>(values);

            var page = values["number"] switch
            {
                "1" => "/one",
                "2" => "/two",
                "3" => "/three",
                _ => "/undefined"
            };

            Console.WriteLine("Route Page");
            foreach (var k in values)
            {
                Console.WriteLine("Key" + k);
            }

            values["page"] = page;

            return new ValueTask<RouteValueDictionary>(values);
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
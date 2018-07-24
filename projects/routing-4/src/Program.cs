using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

namespace Routing4 
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var defaultHandler = new RouteHandler (context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync($"Route values: {string.Join(", ", routeValues)}");
            });

            var routes = new RouteBuilder(app, defaultHandler);
            //This maps 
            // - /hello
            // - /hello/adam
            // - /hello/sasha
            //The ? means that the segement is optional
            routes.MapGet("hello/{name?}", (context) => {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync($"Hello {routeValues["name"]}");
            });

            //This maps
            // - /goodbye (name is assigned value 'bond'. Make sure there is no space between '=' and default value)
            // - /goodbye/xxxx
            //The '=' operator means default value
            routes.MapGet("goodbye/{name=bond}", (context) => {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync($"Goodbye {routeValues["name"]}");
            });

            routes.MapRoute(
                name: "Default", 
                template: "{*path}"
            );
            app.UseRouter(routes.Build());
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
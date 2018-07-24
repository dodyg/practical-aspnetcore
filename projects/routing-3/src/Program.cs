using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

namespace Routing3 
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
            routes.MapRoute(
                name: "hello1",
                template:"hello/{greetings}/from/{country}"
            );
            
            routes.MapRoute(
                name: "hello2",
                template:"hello/{greetings}"
            );

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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

namespace Routing2 
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var routes = new RouteBuilder(app);
            routes.MapGet("", (context) => {
                return context.Response.WriteAsync($"Home Page. Try /good or /good/morning.");
            });

            routes.MapGet("{*path}", (context) =>{
                var routeData = context.GetRouteData();
                var path = routeData.Values;
                return context.Response.WriteAsync($"Path: {string.Join(",",path)}");
            });

            app.UseRouter(routes.Build());
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
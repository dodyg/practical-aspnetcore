using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Routing 
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var defaultHandler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync(
                    $"Hello world! Route values: [{string.Join(", ", routeValues)}] - Path [{context.Request.Path}]. context.GetRouteData() returns nothing regardless what kind of path you are requesting, e.g. '/hello-x'");
            });
            app.UseRouter(defaultHandler);
        }
    }
    
   public class Program
    {
        public static void Main(string[] args)
        {
              var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
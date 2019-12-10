using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

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
                context.Response.Headers.Add("Content-Type", "text/html");
                return context.Response.WriteAsync(
                    $@"
                    <html><head><body>
                    <h1>Routing</h1>
                    Click on the following links to see the changes
                    <ul>
                        <li><a href=""/try"">/try</a></li>
                        <li><a href=""/do/33"">/do/33</a></li>
                        <li><a href=""/values/11/again"">/values/11/again</a></li>
                    </ul> 
                    <br/>
                    Path: {context.Request.Path}. 
                    <br/>
                    <br/>
                    Route values from context.GetRouteData().Values: {string.Join(", ", routeValues)}
                    <br/>
                    Note:
                    <br/>
                    context.GetRouteData() returns nothing regardless what kind of path you are requesting, e.g. '/hello-x'
                    </body></html>
                    ");
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
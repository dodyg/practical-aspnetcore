using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace HelloWorldWithReload 
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app, IInlineConstraintResolver resolver)
        {
            var routes = new RouteBuilder(app);
            routes.Routes.Add(new Route(
            target: new RouteHandler(async ctx => await ctx.Response.WriteAsync("hello world")),
            routeName: "home",
            routeTemplate: "",
            constraints: new Dictionary<string, object>(),
            defaults: new RouteValueDictionary(),
            inlineConstraintResolver: resolver,
            dataTokens: new RouteValueDictionary()));

            IRouter routing = routes.Build();
            app.UseRouter(routing);
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
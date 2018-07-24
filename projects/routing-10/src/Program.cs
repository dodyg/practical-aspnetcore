using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing.Template;
using System.Linq;
using System;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.AspNetCore;

namespace Routing7
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var path1 = "/page/10";
                RouteTemplate template = TemplateParser.Parse("page/{id}");
                var templateMatcher = new TemplateMatcher(template, new RouteValueDictionary());

                var routeData = new RouteValueDictionary();//This dictionary will be populated by the parameter template part (in this case "title")
                var isMatch1 = templateMatcher.TryMatch(path1, routeData);
                await context.Response.WriteAsync($"{path1} is match? {isMatch1} => route data value for 'id' is {routeData["id"]} \n");
                await next.Invoke();
            });

            app.Use(async (context, next) =>
            {
                var path = "/page/gone/a";
                RouteTemplate template = TemplateParser.Parse("page/gone/{id}");
                var templateMatcher = new TemplateMatcher(template, new RouteValueDictionary());

                var routeData = new RouteValueDictionary();//This dictionary will be populated by the parameter template part (in this case "title")
                var isMatch1 = templateMatcher.TryMatch(path, routeData);
                await context.Response.WriteAsync($"{path} is match? {isMatch1} => route data value for 'id' is {routeData["id"]} \n");
            });
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
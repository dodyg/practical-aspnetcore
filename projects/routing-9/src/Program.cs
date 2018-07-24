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
            //We are building a url template from scratch, segment by segemtn, oldskool 
            var segment = new TemplateSegment();
            segment.Parts.Add(TemplatePart.CreateLiteral("page"));

            var segment2 = new TemplateSegment();
            segment2.Parts.Add(TemplatePart.CreateParameter("id",
               isCatchAll: false,
               isOptional: false,
               defaultValue: null,
               inlineConstraints: new InlineConstraint[] { new InlineConstraint("int") }));

            var segments = new TemplateSegment[] {
                segment,
                segment2
            };

            var template = new RouteTemplate("page", segments.ToList());
            var templateMatcher = new TemplateMatcher(template, new RouteValueDictionary());

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("We are using one segment with two parts, one Literal Template Part ('page') and the other with Parameter Template Part ('id').");
                await context.Response.WriteAsync("It is the equivalent of /page/{id:int}");
                await context.Response.WriteAsync("\n\n");
                await next.Invoke();
            });

            app.Use(async (context, next) =>
            {
                var path1 = "/page/10";
                var routeData = new RouteValueDictionary();//This dictionary will be populated by the parameter template part (in this case "title")
                var isMatch1 = templateMatcher.TryMatch(path1, routeData);
                await context.Response.WriteAsync($"{path1} is match? {isMatch1} => route data value for 'id' is {routeData["id"]} \n");
                await next.Invoke();
            });
            
            app.Use(async (context, next) =>
            {
                var path = "/page/a";
                var routeData = new RouteValueDictionary();//This dictionary will be populated by the parameter template part (in this case "title")
                var isMatch1 = templateMatcher.TryMatch(path, routeData);
                await context.Response.WriteAsync($"{path} is match? {isMatch1} - as you can see TemplateMatcher does not give a damn about InlineConstraint. It is by design. \n");
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
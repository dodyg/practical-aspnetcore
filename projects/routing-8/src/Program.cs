using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing.Template;
using System.Linq;
using System;
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
            segment.Parts.Add(TemplatePart.CreateParameter("title", 
                    isCatchAll: true, 
                    isOptional: true, 
                    defaultValue: null, 
                    inlineConstraints: new InlineConstraint[]{})
           );
    
            var segments = new TemplateSegment [] {
                segment
            };

            var template = new RouteTemplate("page", segments.ToList());
            var templateMatcher = new TemplateMatcher(template, new RouteValueDictionary());

            app.Use(async(context, next) => {
                await context.Response.WriteAsync("We are using one segment with two parts, one Literal Template Part ('page') and the other with Parameter Template Part ('title').");
                await context.Response.WriteAsync("It is the equivalent of /page{title}");
                await context.Response.WriteAsync("\n\n");
                await next.Invoke();
            });
            app.Use(async (context, next) => {
                var path1 = "/pagemy-amazing-travel";
                var routeData = new RouteValueDictionary();//This dictionary will be populated by the parameter template part (in this case "title")
                var isMatch1 = templateMatcher.TryMatch(path1, routeData);
                await context.Response.WriteAsync($"{path1} is match? {isMatch1} => route data value for 'title' is {routeData["title"]} \n");
                await next.Invoke();    
            });

            app.Use(async (context, next) => {
                var path1 = "/pagenothing-compares-2-u";
                var routeData = new RouteValueDictionary();//This dictionary will be populated by the parameter template part (in this case "title")
                var isMatch1 = templateMatcher.TryMatch(path1, routeData);
                await context.Response.WriteAsync($"{path1} is match? {isMatch1} => route data value for 'title' is {routeData["title"]} \n");
            });
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
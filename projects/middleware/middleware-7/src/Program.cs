using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            //Use MapMiddleWare and MapWhenMiddlware directly
            var whenOption = new MapWhenOptions
            {
                Branch =
                    context =>
                        context.Response.WriteAsync($"MapWhenMiddleware| Path: {context.Request.Path} - Path Base: {context.Request.PathBase}"),
                Predicate = context => context.Request.Path.Value.Contains("hello")
            };

            app.UseMiddleware<MapWhenMiddleware>(whenOption);

            var mapOption = new MapOptions
            {
                Branch =
                    context =>
                        context.Response.WriteAsync($"MapMiddleware| Path: {context.Request.Path} - Path Base: {context.Request.PathBase}"),
                PathMatch = "/greetings"
            };

            app.UseMiddleware<MapMiddleware>(mapOption);

            app.Run(context =>
            {
                context.Response.Headers.Add("content-type", "text/html");
                return context.Response.WriteAsync(@"
                   <a href=""/hello"">/hello</a> <a href=""/greetings"">/greetings</a>
                ");
            });
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
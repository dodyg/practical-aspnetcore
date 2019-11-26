using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

namespace HelloWorldWithMiddleware
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.MapWhen(context => context.Request.Path.Value.Contains("/hello"), 
            (IApplicationBuilder pp) => {
                //nested
                app.Map("/world", (IApplicationBuilder ppa) => ppa.Run(context => 
                    context.Response.WriteAsync($"Path: {context.Request.Path} - Path Base: {context.Request.PathBase}")));
                
                pp.Run(context => 
                    context.Response.WriteAsync($"Path: {context.Request.Path} - Path Base: {context.Request.PathBase}"));
            });
        
            app.Run(context =>
            { 
                context.Response.Headers.Add("content-type", "text/html");
                return context.Response.WriteAsync(@"
                   <a href=""/hello"">/hello</a> <a href=""/hello/world"">/hello/world</a>
                ");
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
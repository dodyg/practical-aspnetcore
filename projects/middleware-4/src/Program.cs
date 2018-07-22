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
            app.Map("/hello", (IApplicationBuilder pp) => pp.Run(context => context.Response.WriteAsync("Hello")));
            app.Map("/world", (IApplicationBuilder pp) => pp.Run(context => context.Response.WriteAsync("Hello")));
            
            app.Run(context =>
            { 
                context.Response.Headers.Add("content-type", "text/html");
                return context.Response.WriteAsync(@"
                   <a href=""/hello"">hello</a> <a href=""/world"">world</a>
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
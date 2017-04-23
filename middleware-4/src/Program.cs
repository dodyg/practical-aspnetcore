using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

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
            var host = new WebHostBuilder()
              .UseKestrel()
              .UseStartup<Startup>()
              .Build();

            host.Run();
        }
    }
}
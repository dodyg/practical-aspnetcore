using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using System.Threading.Tasks;

namespace HelloWorldWithMiddleware
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            //The order of these things are important. 
            //Only one Middleware should write to the Response. 
            //Do not write to Response before next.Invoke()
            app.Use(async (context, next) =>
            {
                context.Items["Greeting"] = "Hello World";
                await next.Invoke();
                await context.Response.WriteAsync($"{context.Items["Greeting"]}\n");
                await context.Response.WriteAsync($"{context.Items["Goodbye"]}\n");
            });

            app.Use((context, next) =>
            {
                context.Items["Goodbye"] = "Goodbye for now";
                return Task.CompletedTask;
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
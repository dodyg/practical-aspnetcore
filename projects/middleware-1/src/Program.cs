using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;

namespace HelloWorldWithMiddleware
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            //The order of these things are important. 
            app.Use(async (context, next) =>
            {
                context.Items["Greeting"] = "Hello World";
                await next.Invoke();
                await context.Response.WriteAsync($"{context.Items["Goodbye"]}\n");
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync($"{context.Items["Greeting"]}\n");
                context.Items["Goodbye"] = "Goodbye for now";
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
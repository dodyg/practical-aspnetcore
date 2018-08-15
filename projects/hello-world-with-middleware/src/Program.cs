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
                await context.Response.WriteAsync("[1] ----- \n");//1
                await next.Invoke();
                await context.Response.WriteAsync("[5] =====\n");
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("[2] Hello world \n");//2
                await next.Invoke();
                await context.Response.WriteAsync("[4]  \n");//4
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("[3] ----- \n");//3
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
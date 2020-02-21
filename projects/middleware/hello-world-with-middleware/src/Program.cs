using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using System.Threading.Tasks;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            //The order of these things are important. 
            app.Use(async (context, next) =>
            {
                context.Items["Content"] += "[1] ----- \n";//1
                await next.Invoke();
                context.Items["Content"] += "[5] =====\n";//5

                await context.Response.WriteAsync(context.Items["Content"] as string);
            });

            app.Use(async (context, next) =>
            {
                context.Items["Content"] += "[2] Hello world \n";//2
                await next.Invoke();
                context.Items["Content"] += "[4]  \n";//4
            });

            app.Run(context =>
            {
                context.Items["Content"] += "[3] ----- \n";//3
                return Task.CompletedTask;
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
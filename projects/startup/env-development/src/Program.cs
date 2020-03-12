using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.Use(async (context, next) =>
                {

                    await context.Response.WriteAsync("Development Mode \n");
                    await next.Invoke();
                });
            }

            if (env.IsProduction())
            {
                app.Use(async (context, next) =>
                {

                    await context.Response.WriteAsync("Production Mode \n");
                    await next.Invoke();
                });
            }

            if (env.IsStaging())
            {
                app.Use(async (context, next) =>
                {

                    await context.Response.WriteAsync("Staging Mode \n");
                    await next.Invoke();
                });
            }

            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello world");
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
                    webBuilder
                    .UseStartup<Startup>()
                    .UseEnvironment(Environments.Staging) // Other options are Staging and Production
                );
    }
}
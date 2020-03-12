using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, ILogger<Startup> log)
        {
            app.Run(context =>
            {
                log.LogWarning("This is a test log");
                return context.Response.WriteAsync("Hello world. Take a look at your terminal to see the logging messages.");
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
                    .ConfigureLogging(builder =>
                    {
                        builder.SetMinimumLevel(LogLevel.Warning);
                        builder.AddConsole();
                    })
                    .UseStartup<Startup>()
                );
    }
}
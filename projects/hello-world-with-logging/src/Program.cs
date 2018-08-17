using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore;

namespace HelloWorldWithLogging
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            var log = loggerFactory.CreateLogger<Startup>();
            app.Run(context =>
            {
                log.LogTrace($"Request {context.Request.Path}");
                log.LogDebug($"Debug info {context.Request.Path}");
                return context.Response.WriteAsync($"Hello world at {context.Request.Path}");
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
                .ConfigureLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddConsole();
                })
                .UseStartup<Startup>();
    }
}
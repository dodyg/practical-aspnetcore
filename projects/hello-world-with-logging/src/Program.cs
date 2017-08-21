using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

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
            //filter what level of logging you want to see
            var loggerFactory = new LoggerFactory().AddConsole((str, level) =>
            {
                return level >= LogLevel.Trace;
            });

            var log = loggerFactory.CreateLogger<Program>();

            log.LogTrace("Before building host");

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseLoggerFactory(loggerFactory)
                .UseStartup<Startup>()
                .Build();

            log.LogTrace("After building host");

            host.Run();

            log.LogInformation("Host is shut down");
        }
    }
}
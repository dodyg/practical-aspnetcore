using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HelloWorldWithLogging 
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            //filter what level of logging you want to see
            //filter out framework logging
            loggerFactory.AddConsole((str, level) => {
                    return !str.Contains("Microsoft.AspNetCore") && level >= LogLevel.Trace;
            });

            var log = loggerFactory.CreateLogger("Startup");
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
              var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
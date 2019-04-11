using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;

namespace StartupBasic 
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are two services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //These are the four default services available at Configure
            var log = logger.CreateLogger("AppLogger");
            app.Run(context =>
            {
                log.LogInformation("This is a information message");
                log.LogDebug("This is debug message");
                return context.Response.WriteAsync(configuration["greeting"]);
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
                .ConfigureLogging(builder =>
                {
                    // Trace, Debug, Information, Warning, Error, Critical, None
                    builder.AddFilter("Microsoft", LogLevel.Warning); //Only show Warning log and above from anything that contains Microsoft.
                    builder.AddFilter("AppLogger", LogLevel.Trace);//Pretty much show everything from AppLogger
                    builder.AddConsole();
                })
                .UseEnvironment("Development");
    }
}
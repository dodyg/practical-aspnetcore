using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Modules.HelloWorld;

namespace Module 
{
    public class Startup
    {
        IConfiguration _configuration;
        public Startup(IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddModules();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();    
            }
            app.UseModules();
        }
    }
    
   public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(builder =>
                {
                    // Trace, Debug, Information, Warning, Error, Critical, None
                    builder.AddFilter("Microsoft", LogLevel.Warning); //Only show Warning log and above from anything that contains Microsoft.
                    builder.AddFilter("AppLogger", LogLevel.Trace);//Pretty much show everything from AppLogger
                    builder.AddConsole();
                })
                .UseEnvironment("Development")
                .Build();
    }
}
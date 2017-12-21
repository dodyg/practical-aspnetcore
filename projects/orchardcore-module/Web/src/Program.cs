using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;

namespace Module 
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddModules();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
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
                .UseEnvironment("Development")
                .Build();
    }
}
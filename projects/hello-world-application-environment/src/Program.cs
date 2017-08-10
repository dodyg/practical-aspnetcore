using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace HelloWorldWithApplicationEnvironment
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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure
            
            app.Run(async context =>
            {
                context.Response.Headers.Add("content-type", "text/html");
                var environment = PlatformServices.Default.Application;
                await context.Response.WriteAsync($"Application Name: {environment.ApplicationName}<br/>");
                await context.Response.WriteAsync($"Application Base Path: {environment.ApplicationBasePath}<br/>");
                await context.Response.WriteAsync($"Target Framework: {environment.RuntimeFramework}<br/>");
                await context.Response.WriteAsync($"Version: {environment.ApplicationVersion}<br/>");
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
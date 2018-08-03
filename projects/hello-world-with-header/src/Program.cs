using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore;

namespace StartupWithHeader 
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
                await context.Response.WriteAsync("<b>Hello world</b>");
                try
                {
                    context.Response.Headers.Add("my-header", "awesome");
                }
                catch(Exception ex)
                {
                    await context.Response.WriteAsync($"<br/><br/>You cannot modify header collections after the body is sent already. Exception: {ex.Message}");
                }
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
                .UseEnvironment("Development");
    }
}
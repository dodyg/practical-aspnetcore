using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;

namespace HelloWorldWithApplicationEnvironment
{
    // https://github.com/aspnet/Announcements/issues/237
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

            app.Run(async context =>
            {
                context.Response.Headers.Add("content-type", "text/html");
                await context.Response.WriteAsync($"Application Name: {System.Reflection.Assembly.GetEntryAssembly().GetName().Name}<br/>");
                await context.Response.WriteAsync($"Application Base Path: {System.AppContext.BaseDirectory}<br/>");

                System.Reflection.Assembly entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
                var targetFramework = entryAssembly.GetCustomAttributes(typeof(System.Runtime.Versioning.TargetFrameworkAttribute), true)[0] as System.Runtime.Versioning.TargetFrameworkAttribute;
                await context.Response.WriteAsync($"Target Framework: {targetFramework.FrameworkName}<br/>");

                await context.Response.WriteAsync($"Version: {System.Reflection.Assembly.GetEntryAssembly().GetName().Version}<br/>");
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
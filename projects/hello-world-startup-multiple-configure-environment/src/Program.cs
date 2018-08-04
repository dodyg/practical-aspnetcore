using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

namespace StartupBasic 
{
    public class Startup
    {
        public void ConfigureProduction(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure
            
            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello world Production");
            });
        }

        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure
            
            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello world Development");
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
                .UseEnvironment("Development"); // You can change this to "Production" and it will use ConfigureProduction
    }
}
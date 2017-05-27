using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace StartupBasicMultipleEnvironment
{
    public class StartupProduction
    {
        public StartupProduction(IHostingEnvironment env, ILoggerFactory logger)
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

            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello world from Startup Production");
            });
        }
    }

    public class StartupDevelopment
    {
        public StartupDevelopment(IHostingEnvironment env, ILoggerFactory logger)
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

            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello world Startup Development");
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
              .UseStartup(typeof(Program).Assembly.FullName) 
              .UseEnvironment("Development") //switch to "Production" to use StartupProduction
              .UseKestrel();
            host.Build().Run();
        }
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            if (env.IsDevelopment()){
                app.Use(async (context, next) =>{

                    await context.Response.WriteAsync("Development Mode \n");
                    await next.Invoke();
                });
            }

            if (env.IsProduction()){
                app.Use(async (context, next) =>{

                    await context.Response.WriteAsync("Production Mode \n");
                    await next.Invoke();
                });
            }

            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello world");
            });
        }
    }
    
   public class Program
    {
        public static void Main(string[] args)
        {
              var host = new WebHostBuilder()
                .UseKestrel()
                .UseEnvironment("Development") //Change to 'Production' if you want to make it in production mode
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
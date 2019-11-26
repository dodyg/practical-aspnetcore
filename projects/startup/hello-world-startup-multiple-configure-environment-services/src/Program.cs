using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

namespace StartupBasicConfigureEnvironment 
{
    public class Greet
    {
        string _greeting;

        public Greet(string greeting)
        {
            _greeting = greeting;
        }

        public string Say() => _greeting;
    }

    public class Startup
    {
        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddSingleton(new Greet("Hello Production"));
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddSingleton(new Greet("Hello Development"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure
            
            app.Run(context =>
            {
                var greeter = context.RequestServices.GetService<Greet>();
                if (greeter == null)
                    return context.Response.WriteAsync($"Greeter is null");
                else
                    return context.Response.WriteAsync($"{greeter.Say()}");
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
                .UseEnvironment("Development") //switch to "Production" to use StartupProduction
                .Build();

            host.Run();
        }
    }
}
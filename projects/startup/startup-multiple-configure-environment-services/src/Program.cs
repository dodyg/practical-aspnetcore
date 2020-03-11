using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
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

        public void Configure(IApplicationBuilder app)
        {
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
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                    .UseEnvironment(Environments.Development)
                ); // You can change this to "Production" and it will use ConfigureProduction
    }
}
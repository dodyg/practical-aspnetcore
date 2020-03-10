using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Greeter
    {
        public string Say() => "Look Ma, no Startup class";
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
                    webBuilder
                    .ConfigureServices(services => 
                    {
                        services.AddSingleton<Greeter>();
                    })
                    .Configure(app =>
                    {
                        app.Run(context =>
                        {
                            var greet = context.RequestServices.GetService<Greeter>();
                            return context.Response.WriteAsync($"{greet.Say()}");
                        });
                    })
                );
    }
}
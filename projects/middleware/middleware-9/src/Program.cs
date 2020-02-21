using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

namespace PracticalAspNetCore
{
    public class Greeting
    {
        public string Greet() => "Good morning";
    }

    public class Goodbye
    {
        public string Say() => "Goodbye";
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Greeting>();
            services.AddSingleton<Goodbye>();
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(TerminalMiddleware));
        }
    }

    public class TerminalMiddleware
    {
        Greeting _greet;

        public TerminalMiddleware(RequestDelegate next, Greeting greet)
        {
            _greet = greet;
        }

        public async Task Invoke(HttpContext context, Goodbye goodbye)
        {
            await context.Response.WriteAsync($"{_greet.Greet()} {goodbye.Say()}");
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
                );
    }
}

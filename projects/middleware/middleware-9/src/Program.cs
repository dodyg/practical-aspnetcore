using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

namespace HelloWorldWithMiddleware
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
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}

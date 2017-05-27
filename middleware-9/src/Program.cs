using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace HelloWorldWithMiddleware
{
    public class Greeting
    {
        public string Greet() => "Good morning";
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Greeting>();
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

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{_greet.Greet()}");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
              .UseKestrel()
              .UseStartup<Startup>()
              .Build();

            host.Run();
        }
    }
}

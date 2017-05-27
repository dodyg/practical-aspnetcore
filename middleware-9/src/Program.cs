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
            //Pay attention to the order of the parameter passed.
            //If your parameter is distinct, the order does not matter, As you can see here we put Greeting
            //at the end of the parameter passing although in the constructor Greeting was the second on the parameter list.
            //However if you pass multiple parameters of the same type, the order matters. 
            app.UseMiddleware(typeof(TerminalMiddleware));
        }
    }

    public class TerminalMiddleware
    {

        public TerminalMiddleware(RequestDelegate next)
        {
            //We are not using the parameter next in this middleware since this middleware is terminal
        }

        public string Greet(Greeting greet) => "hello";
        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{Greet(null)}");
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
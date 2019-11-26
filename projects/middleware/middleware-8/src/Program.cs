using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

namespace HelloWorldWithMiddleware
{
    public class Content
    {
        public string SayHello() => "Hello world";
    }

    public class Greeting
    {
        public string Greet() => "Good morning";
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Content>();
        }
        
        public void Configure(IApplicationBuilder app)
        {
            //Pay attention to the order of the parameter passed.
            //If your parameter is distinct, the order does not matter, As you can see here we put Greeting
            //at the end of the parameter passing although in the constructor Greeting was the second on the parameter list.
            //However if you pass multiple parameters of the same type, the order matters. 
            app.UseMiddleware<TerminalMiddleware>("Cairo", "Dody", new Greeting());
        }
    }

    public class TerminalMiddleware
    {
        readonly Content _content;
        readonly string _city;
        readonly Greeting _greet;
        readonly string _name;

        public TerminalMiddleware(RequestDelegate next, Greeting greet, string city, Content cnt, string name)
        {
            //We are not using the parameter next in this middleware since this middleware is terminal
            _content = cnt;
            _city = city;
            _greet = greet;
            _name = name;
        }
        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{_greet.Greet()} {_name}, {_content.SayHello()} from {_city}");
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
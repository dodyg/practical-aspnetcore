using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore;

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
            //Register all objects configured by classes that implements IBootstrap.
            //This is useful when you have large amount of classes in your project that needs
            //registration. You can register them near where they are (usually in the same folder) instead of
            //registering them somewhere in a giant registration function
            var type = typeof(IBootstrap);
            var types = System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass);

            foreach(var p in types)
            {
                var config = (IBootstrap)System.Activator.CreateInstance(p);
                config.Register(services);
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure
            
            app.Run(context =>
            {
                var person = app.ApplicationServices.GetService<Person>();
                var greeting = app.ApplicationServices.GetService<Greeting>();

                return context.Response.WriteAsync($"{greeting.Message} {person.Name}");
            });
        }
    }

    public interface IBootstrap
    {
        void Register(IServiceCollection services);
    }

    public class Registration1 : IBootstrap
    {
        public void Register(IServiceCollection services)
        {
            services.AddTransient(x => new Person { Name = "Mahmoud"});
            //continue registering all your classes here
        }
    }

    //This is a contrite sample but it demonstrates that you can have these two registration classes in farflung folders near the classes
    //they are registrating.
    public class Registration2 : IBootstrap
    {
        public void Register(IServiceCollection services)
        {
            services.AddTransient(x => new Greeting { Message = "Good Morning"});
           //continue registering all your classes here
        }
    }

    public class Person 
    {
        public string Name { get; set;}
    }

    public class Greeting
    {
        public string Message { get; set;}
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
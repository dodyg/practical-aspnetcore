using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;

namespace Configuration 
{
    public class Startup
    {
        IConfigurationRoot _config;

        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //This is the most basic configuration you can have
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(); //if you remove this, you are gonna get error 

            _config = builder.Build();
            _config["message"] = "hello world";
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            app.Run(context =>
            {
                return context.Response.WriteAsync($"{_config["message"]}");
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
                );
    }
}
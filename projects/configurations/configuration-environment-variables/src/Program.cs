using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore 
{
    public class Startup
    {
        IConfigurationRoot _config;

        public Startup()
        {
            //This is the most basic configuration you can have
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            _config = builder.Build();

        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                foreach(var e in _config.AsEnumerable())
                {
                    await context.Response.WriteAsync($"{e.Key} = {e.Value}\n");
                }
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
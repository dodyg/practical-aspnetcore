using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;

namespace HelloWorldWithIApplicationLifetime 
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IApplicationLifetime lifetime)
        {
        
            lifetime.ApplicationStarted.Register(() => System.Console.WriteLine("===== Server is starting"));
            lifetime.ApplicationStopping.Register(() => System.Console.WriteLine("===== Server is stopping"));
            lifetime.ApplicationStopped.Register(() => System.Console.WriteLine("===== Server has stopped"));
            
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello world");
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
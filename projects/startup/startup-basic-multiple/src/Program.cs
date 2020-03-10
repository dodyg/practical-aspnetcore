using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup1
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello world from Startup1");
            });
        }
    }

    public class Startup2
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello world Startup2");
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
                {
                     //This is a dumb way of doing it. You can use command line argument, etc to switch your startup 
                     var startupNumber = 1; //CHANGE THIS to 2 if you want to use Startup2

                     if (startupNumber == 1)
                        webBuilder.UseStartup<Startup1>();
                     else if (startupNumber == 2)
                        webBuilder.UseStartup<Startup2>();
                });
    }
}
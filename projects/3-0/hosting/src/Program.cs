using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;

namespace HelloWorldWithReload 
{   
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
                    webBuilder
                    .Configure(app =>{
                        app.Run(context =>
                        {
                            return context.Response.WriteAsync("Hello world. Make sure you run this app using 'dotnet watch run'.");
                        });
                    })
                    .ConfigureServices(services => {
                        services.AddResponseCompression();
                    })
                    .ConfigureLogging(logging => {
                    })
                    .CaptureStartupErrors(true)
                    .SuppressStatusMessages(true)
                    .UseEnvironment("Development");
                });
    }
}
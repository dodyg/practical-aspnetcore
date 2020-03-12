using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Wangkanai.Detection;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDetectionCore()
                .AddDevice();
        }

        public void Configure(IApplicationBuilder app)
        {
            //These are the four default services available at Configure
            app.Run(context =>
            {
                var device = context.RequestServices.GetService<IDeviceResolver>();

                return context.Response.WriteAsync($@"
                Useragent : {device.UserAgent}
                Device Type: {device.Device.Type}
                ");
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
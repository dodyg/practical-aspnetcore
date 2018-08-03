using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
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
            //This is the only service available at ConfigureServices
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure
           
            app.Run(context =>
            {
                var connection = context.Connection;
                var str = string.Empty;
                str += $"Local IP:Port => {connection.LocalIpAddress}:{connection.LocalPort}\n";
                str += $"Remote IP:Port => {connection.RemoteIpAddress}:{connection.RemotePort}\n";
                str += $"ClientCertificate : {connection.ClientCertificate?.FriendlyName}\n";

                return context.Response.WriteAsync($"{str}");
            });
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
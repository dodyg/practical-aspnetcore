using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var connection = context.Connection;
                var str = string.Empty;
                str += $"Local IP:Port => {connection.LocalIpAddress}:{connection.LocalPort}\n";
                str += $"Remote IP:Port => {connection.RemoteIpAddress}:{connection.RemotePort}\n";
                str += $"Client Certificate => {connection.ClientCertificate?.FriendlyName}\n";

                return context.Response.WriteAsync($"{str}");
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
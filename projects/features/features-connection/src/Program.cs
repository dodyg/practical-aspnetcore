using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore;

namespace Features.Connection 
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var connection = context.Features.Get<IHttpConnectionFeature>();
                var str = string.Empty;
                str += $"Local IP:Port : {connection.LocalIpAddress}:{connection.LocalPort}\n";
                str += $"Remote IP:Port : {connection.RemoteIpAddress}:{connection.RemotePort}\n";
                str += $"Connection Id : {connection.ConnectionId}\n";

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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Grpc.Core;
using System;

namespace GrpcServer
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                return context.Response.WriteAsync("This server contains a GRPC service");
            });
        }
    }

    public class BillboardService : Billboard.Board.BoardBase
    {
        public override Task<Billboard.MessageReply> ShowMessage(Billboard.MessageRequest request, ServerCallContext context)
        {
            var now = DateTime.UtcNow;
            return new Billboard.MessageReply
            {
                DisplayTime = now.ToTicks()
            };
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
                    webBuilder.UseStartup<Startup>().
                    ConfigureKestrel(k =>
                    {
                        k.ListenLocalhost(5500);
                    }).
                    UseEnvironment(Environments.Development);
                });
    }
}
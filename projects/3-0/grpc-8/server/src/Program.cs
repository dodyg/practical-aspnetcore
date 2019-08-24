using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Grpc.Core;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Text;

namespace GrpcServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<BillboardService>();
                endpoints.MapGet("/", context =>
                {
                    return context.Response.WriteAsync("This server contains a gRPC service");
                });
            });
        }
    }

    public class BillboardService : Billboard.Board.BoardBase
    {
        public override Task<Billboard.MessageReply> ShowMessage(Billboard.MessageRequest request, ServerCallContext context)
        {
            var message = new StringBuilder();
            message.AppendLine($"WageCase {request.WageCase}");
            message.AppendLine($"WageValue {request.WageValue}");
            message.AppendLine($"BonusCase {request.BonusCase}");
            if (request.BonusCase == Billboard.MessageRequest.BonusOneofCase.None)
            {
                message.AppendLine("BonusCase None means that BonusValue property is never set by the requester. So you can ignore whatever values you see at BonusValue property.");
            }
            message.AppendLine($"BonusValue {request.BonusValue}");

            var now = DateTime.UtcNow;
            return Task.FromResult(new Billboard.MessageReply
            {
                ReceivedTime = now.Ticks,
                Message = message.ToString()
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
                    webBuilder.UseStartup<Startup>().
                    ConfigureKestrel(k =>
                    {
                        k.ConfigureEndpointDefaults(options =>
                        {
                            options.Protocols = HttpProtocols.Http2;
                        });

                        k.ListenLocalhost(5500);
                    }).
                    UseEnvironment(Environments.Development);
                });
    }
}
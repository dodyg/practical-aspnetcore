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
            var now = DateTime.UtcNow;
            return Task.FromResult(new Billboard.MessageReply
            {
                DisplayTime = now.Ticks,
                ReceiveFrom = request.Sender
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
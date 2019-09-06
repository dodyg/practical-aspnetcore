using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Grpc.Core;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Billboard;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

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

    public class ReceivedFortune
    {
        public string Message { get; set; }

        public DateTimeOffset Received { get; set; } = DateTimeOffset.UtcNow;
    }

    public class BillboardService : Billboard.Board.BoardBase
    {
        public override async Task<MessageReply> ShowMessage(IAsyncStreamReader<MessageRequest> requestStream, ServerCallContext context)
        {
            var fortunes = new List<ReceivedFortune>();

            using var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            await foreach (var request in requestStream.ReadAllAsync(token))
            {
                var inBed = request.FortuneCookie[0..^1] + " in bed.";

                fortunes.Add(new ReceivedFortune { Message = inBed });
            }

            var reply = new MessageReply();

            foreach (var f in fortunes)
            {
                reply.Fortunes.Add(new TranslatedFortune
                {
                    Message = f.Message,
                    ReceivedTime = f.Received.Ticks
                });
            }

            return reply;
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
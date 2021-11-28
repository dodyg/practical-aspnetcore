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
using System.IO;
using System.Threading;
using Google.Protobuf;

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

        IHostEnvironment _env;

        public BillboardService(IHostEnvironment env)
        {
            _env = env;
        }

        public override async Task ShowMessage(Google.Protobuf.WellKnownTypes.Empty _, IServerStreamWriter<MessageReply> responseStream, ServerCallContext context)
        {
            using var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            var meta = new MessageReply();
            var metaData = new ImageMetaData();
            metaData.FileName = "kitty.jpg";
            metaData.MimeType = "image/jpeg";
            meta.MetaData = metaData;

            await responseStream.WriteAsync(meta);

            var kitty = Path.Combine(_env.ContentRootPath, "kitty.jpg");

            using var reader = new FileStream(kitty, FileMode.Open);

            int chunkSize = 100;
            int bytesRead;
            byte[] buffer = new byte[chunkSize];
            int position = 0;
            long length = reader.Length;
            while ((bytesRead = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                var reply = new MessageReply();
                var chunk = new ImageChunk();
                chunk.Length = bytesRead;
                chunk.Data = ByteString.CopyFrom(buffer);
                reply.Chunk = chunk;

                await responseStream.WriteAsync(reply);

                position += bytesRead;
                Console.WriteLine(position);
            }
            reader.Close();
            Console.WriteLine("Done");
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
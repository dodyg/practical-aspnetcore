using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;
using System.Net;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System.IO.Pipelines;
using System;
using System.Buffers;
using System.Threading.Tasks;

namespace EchoServer
{
    public class EchoConnectionHandler : ConnectionHandler
    {
        public override async Task OnConnectedAsync(ConnectionContext connection)
        {
            while (true)
            {
                ReadResult result = await connection.Transport.Input.ReadAsync();
                ReadOnlySequence<byte> buffer = result.Buffer;

                foreach (ReadOnlyMemory<byte> x in buffer)
                {
                    await connection.Transport.Output.WriteAsync(x);
                }

                if (result.IsCompleted)
                    break;

                connection.Transport.Input.AdvanceTo(buffer.End);
            }
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app)
        {
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
            .ConfigureWebHost(config =>
            {
                config.UseKestrel(k =>
                {
                    k.ListenLocalhost(8007, builder =>
                    {
                        builder.UseConnectionHandler<EchoConnectionHandler>();
                    });
                })
                .UseStartup<Startup>();
            })
            ;
    }
}
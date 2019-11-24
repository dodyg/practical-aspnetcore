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
using Microsoft.Extensions.Logging;
using System.Threading;

namespace EchoServer
{
    // Code extracted from https://github.com/davidfowl/MultiProtocolAspNetCore
    public class EchoConnectionHandler : ConnectionHandler
    {
        readonly ILogger<EchoConnectionHandler> _log;

        public EchoConnectionHandler(ILogger<EchoConnectionHandler> log)
        {
            _log = log;
        }

        public override async Task OnConnectedAsync(ConnectionContext connection)
        {
            try
            {
                _log.LogDebug("Receive connection on " + connection.ConnectionId);

                while (true)
                {
                    ReadResult result = await connection.Transport.Input.ReadAsync();
                    ReadOnlySequence<byte> buffer = result.Buffer;
                    
                    _log.LogDebug("Receiving data on " + connection.ConnectionId);

                    foreach (ReadOnlyMemory<byte> x in buffer)
                    {
                        await connection.Transport.Output.WriteAsync(x);
                    }

                    if (result.IsCompleted)
                    {
                        _log.LogDebug("result.IsCompleted " + connection.ConnectionId);
                        break;
                    }

                    connection.Transport.Input.AdvanceTo(buffer.End);
                }
            }
            finally
            {
                connection.Transport.Input.Complete();
                connection.Transport.Output.Complete();
                _log.LogDebug($"Connection {connection.ConnectionId} disconnected");
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
                    k.ListenLocalhost(8087, builder =>
                    {
                        builder.UseConnectionHandler<EchoConnectionHandler>();
                    });
                })
                .UseStartup<Startup>();
            }).ConfigureLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Debug);
                builder.AddConsole();
            });
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using Grpc.Net.Client;
using Grpc.Core;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcServer
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            //Make sure that the grpc-server is run 
            app.Run(async context =>
            {
                //We need this switch because we are connecting to an unsecure server. If the server runs on SSL, there's no need for this switch.
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:5500"); //check the values at /server project
                var client = new Billboard.Board.BoardClient(channel);

                context.Response.Headers["Content-Type"] = "text/event-stream";

                using var tokenSource = new CancellationTokenSource();
                CancellationToken token = tokenSource.Token;

                using var stream = client.ShowMessage(cancellationToken: token);

                bool response = true;
                int inc = 1;
                do
                {
                    try
                    {
                        var delay = checked(1000 * inc);

                        await stream.RequestStream.WriteAsync(new Billboard.MessageRequest { Ping = "Ping", DelayTime = delay });
                        inc++;
                        await context.Response.WriteAsync($"Send ping on {DateTimeOffset.UtcNow} \n");
                        response = await stream.ResponseStream.MoveNext(token);
                        if (response)
                        {
                            var result = stream.ResponseStream.Current;
                            await context.Response.WriteAsync($"Receive {result.Pong} on {DateTimeOffset.UtcNow} \n\n");
                        }
                    }
                    catch (System.OverflowException)
                    {
                        inc = 1;
                    }
                }
                while (response);
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
                    UseEnvironment(Environments.Development);
                });
    }
}
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
        List<string> Fortunes { get; set; } = new List<string>()
        {
            "The fortune you seek is in another cookie.",
            "A closed mouth gathers no feet.",
            "A conclusion is simply the place where you got tired of thinking.",
            "A cynic is only a frustrated optimist.",
            "A foolish man listens to his heart. A wise man listens to cookies.",
            "You will die alone and poorly dressed.",
            "A fanatic is one who can't change his mind, and won't change the subject.",
            "If you look back, you'll soon be going that way.",
            "You will live long enough to open many fortune cookies.",
            "An alien of some sort will be appearing to you shortly."
        };

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

                foreach (var f in Fortunes)
                {
                    if (token.IsCancellationRequested)
                        break;

                    await stream.RequestStream.WriteAsync(new Billboard.MessageRequest
                    {
                        FortuneCookie = f
                    });

                    await context.Response.WriteAsync($"Sending \"{f}\" \n");
                    await context.Response.Body.FlushAsync();

                    await Task.Delay(1000); //1 second
                }

                await stream.RequestStream.CompleteAsync();

                var response = await stream.ResponseAsync;

                await context.Response.WriteAsync("\n\n");

                foreach (var r in response.Fortunes)
                {
                    await context.Response.WriteAsync($"Reply \"{r.Message}\". Original cookied received on {new DateTime(r.ReceivedTime)}. \n");
                }
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
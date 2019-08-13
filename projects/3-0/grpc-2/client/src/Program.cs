using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using Grpc.Net.Client;
using Grpc.Core;
using System.Threading;

namespace GrpcServer
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            //Make sure that the grpc-server is run 
            app.Run(async context =>
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://localhost:5500"); //check the values at /server project
                var client = GrpcClient.Create<Billboard.Board.BoardClient>(httpClient);
                var result = client.ShowMessage(new Billboard.MessageRequest
                {
                    Name = "Johny"
                });

                context.Response.Headers["Content-Type"] = "text/event-stream";

                using var tokenSource = new CancellationTokenSource();
                CancellationToken token = tokenSource.Token;

                var streamReader = result.ResponseStream;

                while (await streamReader.MoveNext(token))
                {
                    var reply = streamReader.Current;

                    var displayDate = new DateTime(reply.DisplayTime);
                    await context.Response.WriteAsync($"Received \"{reply.Message}\" on {displayDate.ToLongTimeString()} \n");
                    await context.Response.Body.FlushAsync();
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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using Grpc.Net.Client;

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
                var reply = await client.ShowMessageAsync(new Billboard.MessageRequest
                {
                    Message = "Hello World",
                    Sender = "Dody Gunawinata",
                    Type = Billboard.MessageRequest.Types.MessageType.LongForm
                });

                var displayDate = new DateTime(reply.ReceivedTime);
                await context.Response.WriteAsync($"We sent a message to a gRPC server and  received  the following reply '{reply.Message}' on {displayDate} ");
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
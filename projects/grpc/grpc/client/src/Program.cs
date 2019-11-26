using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System;
using Grpc.Net.Client;
using System.Net.Http;

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
                var channel = GrpcChannel.ForAddress("http://localhost:5500");
                var client = new Billboard.Board.BoardClient(channel);
                var reply = await client.ShowMessageAsync(new Billboard.MessageRequest
                {
                    Message = "Good morning people of the world",
                    Sender = "Dody Gunawinata"
                });

                var displayDate = new DateTime(reply.DisplayTime);
                await context.Response.WriteAsync($"This server sends a gRPC request to a server and get the following result: Received message on {displayDate} from {reply.ReceiveFrom}");
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
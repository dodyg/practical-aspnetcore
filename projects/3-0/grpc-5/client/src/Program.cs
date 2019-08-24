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
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://localhost:5500"); //check the values at /server project
                var client = GrpcClient.Create<Billboard.Board.BoardClient>(httpClient);
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
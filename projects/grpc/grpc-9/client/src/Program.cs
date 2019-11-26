using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using Grpc.Net.Client;
using Grpc.Core;
using System.Threading;
using System.IO;

namespace GrpcServer
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            //Make sure that the grpc-server is run 
            app.Run(async context =>
            {
                //We need this switch because we are connecting to an unsecure server. If the server runs on SSL, there's no need for this switch.
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

                var channel = GrpcChannel.ForAddress("http://localhost:5500"); //check the values at /server project
                var client = new Billboard.Board.BoardClient(channel);
                var result = client.ShowMessage(new Google.Protobuf.WellKnownTypes.Empty());

                context.Response.Headers["Content-Type"] = "text/html";

                await context.Response.WriteAsync("<html><body><h1>Kitty Streaming</h1>");

                using var tokenSource = new CancellationTokenSource();
                CancellationToken token = tokenSource.Token;

                var streamReader = result.ResponseStream;

                //get metdata
                string path = null;
                await streamReader.MoveNext(token);
                var initial = streamReader.Current;
                if (initial.ImageCase == Billboard.MessageReply.ImageOneofCase.MetaData)
                {
                    path = Path.Combine(env.WebRootPath, initial.MetaData.FileName);
                }

                if (path == null)
                    throw new ApplicationException("Metadata is missing from the server");

                using FileStream file = new FileStream(path, FileMode.Create);

                int position = 0;
                await foreach (var data in streamReader.ReadAllAsync(token))
                {
                    var chunk = data.Chunk;
                    await file.WriteAsync(chunk.Data.ToByteArray(), 0, chunk.Length);
                    position += chunk.Length;
                    await context.Response.WriteAsync(position + " ");
                }

                file.Close();

                await context.Response.WriteAsync($"<img src=\"{initial.MetaData.FileName}\"/>");

                await context.Response.WriteAsync("</body></html>");
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
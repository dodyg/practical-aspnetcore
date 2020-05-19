using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GrpcClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(x =>
            {
                var handler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
                var channel = GrpcChannel.ForAddress("http://localhost:5500", new GrpcChannelOptions
                {
                    HttpClient = new HttpClient(handler)
                });

                var client = new Billboard.Board.BoardClient(channel);
                return client;
            });

            await builder.Build().RunAsync();
        }
    }
}
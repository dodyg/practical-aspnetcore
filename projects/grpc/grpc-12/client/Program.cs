using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GrpcClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");

builder.Services.AddSingleton(x =>
{
    var handler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
    var channel = GrpcChannel.ForAddress("https://localhost:5500", new GrpcChannelOptions
    {
        HttpClient = new HttpClient(handler)
    });

    var client = new Billboard.Board.BoardClient(channel);
    return client;
});

var app = builder.Build();
await app.RunAsync();
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Grpc.Core;

var app = WebApplication.Create();

//Make sure that the grpc-server is run 
app.Run(async context =>
{
    var handler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
    var channel = GrpcChannel.ForAddress("https://localhost:5500", new GrpcChannelOptions
    {
        HttpClient = new HttpClient(handler)
    });

    var client = new Billboard.Board.BoardClient(channel);
    var result = client.ShowMessage(new Billboard.MessageRequest
    {
        Name = "Anne"
    });

    context.Response.Headers["Content-Type"] = "text/event-stream";

    using var tokenSource = new CancellationTokenSource();
    CancellationToken token = tokenSource.Token;

    var streamReader = result.ResponseStream;

    await foreach (var reply in streamReader.ReadAllAsync(token))
    {
        var displayDate = new DateTime(reply.DisplayTime);
        await context.Response.WriteAsync($"Received \"{reply.Message}\" on {displayDate.ToLongTimeString()} \n");
        await context.Response.Body.FlushAsync();
    }
});

app.Run();
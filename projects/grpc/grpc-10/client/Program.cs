using Grpc.Net.Client;
using Grpc.Net.Client.Web;

var app = WebApplication.Create();

app.Run(async context =>
{
    var handler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
    var channel = GrpcChannel.ForAddress("https://localhost:5500", new GrpcChannelOptions
    {
        HttpClient = new HttpClient(handler)
    });

    var client = new Billboard.Board.BoardClient(channel);

    var reply = await client.ShowMessageAsync(new Billboard.MessageRequest
    {
        Message = "Good morning people of the world",
        Sender = "Dody Gunawinata"
    });

    Console.WriteLine("Connecting");

    var displayDate = new DateTime(reply.DisplayTime);
    await context.Response.WriteAsync($"This server sends a gRPC request to a server and get the following result: Received message on {displayDate} from {reply.ReceiveFrom}");
});

app.Run();
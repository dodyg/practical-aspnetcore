using Grpc.Net.Client;

var app = WebApplication.Create();

app.Run(async context =>
{
    var channel = GrpcChannel.ForAddress("https://localhost:5500");
    var client = new Billboard.Board.BoardClient(channel);
    var reply = await client.ShowMessageAsync(new Billboard.MessageRequest
    {
        Message = "Good morning people of the world",
        Sender = "Dody Gunawinata"
    });

    var displayDate = new DateTime(reply.DisplayTime);
    await context.Response.WriteAsync($"This server sends a gRPC request to a server and get the following result: Received message on {displayDate} from {reply.ReceiveFrom}");
});

app.Run();

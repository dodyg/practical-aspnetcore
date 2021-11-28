using Grpc.Net.Client;

var app = WebApplication.Create();

//Make sure that the grpc-server is run 
app.Run(async context =>
{
    var channel = GrpcChannel.ForAddress("https://localhost:5500"); //check the values at /server project
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

app.Run();
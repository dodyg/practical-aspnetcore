using Grpc.Net.Client;

var app = WebApplication.Create();

//Make sure that the grpc-server is run 
app.Run(async context =>
{
    var channel = GrpcChannel.ForAddress("https://localhost:5500"); //check the values at /server project
    var client = new Billboard.Board.BoardClient(channel);
    var request = new Billboard.MessageRequest();
    request.WageValue = 10_000;

    var reply = await client.ShowMessageAsync(request);

    var displayDate = new DateTime(reply.ReceivedTime);
    await context.Response.WriteAsync($"We sent a message to a gRPC server and  received  the following reply \n'\n{reply.Message}' \non {displayDate} ");
});

app.Run();
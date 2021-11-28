using Grpc.Net.Client;

var app = WebApplication.Create();
app.Run(async context =>
{
    var channel = GrpcChannel.ForAddress("https://localhost:5500"); //check the values at /server project
    var client = new Billboard.Board.BoardClient(channel);

    context.Response.Headers["Content-Type"] = "text/event-stream";

    using var tokenSource = new CancellationTokenSource();
    CancellationToken token = tokenSource.Token;

    using var stream = client.ShowMessage(cancellationToken: token);

    bool response = true;
    int inc = 1;
    do
    {
        try
        {
            var delay = checked(1000 * inc);

            await stream.RequestStream.WriteAsync(new Billboard.MessageRequest { Ping = "Ping", DelayTime = delay });
            inc++;
            await context.Response.WriteAsync($"Send ping on {DateTimeOffset.UtcNow} \n");
            response = await stream.ResponseStream.MoveNext(token);
            if (response)
            {
                var result = stream.ResponseStream.Current;
                await context.Response.WriteAsync($"Receive {result.Pong} on {DateTimeOffset.UtcNow} \n\n");
            }
        }
        catch (System.OverflowException)
        {
            inc = 1;
        }
    }
    while (response);
});

app.Run();

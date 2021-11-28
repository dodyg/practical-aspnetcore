using Billboard;
using Grpc.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder();
builder.Services.AddGrpc();

builder.WebHost.ConfigureKestrel(k =>
{
    k.ConfigureEndpointDefaults(options => options.Protocols = HttpProtocols.Http2);
    k.ListenLocalhost(5500, o => o.UseHttps());
});

var app = builder.Build();

app.MapGrpcService<BillboardService>();
app.MapGet("/", () => "This server contains a gRPC service");

app.Run();

public class ReceivedFortune
{
    public string Message { get; set; }

    public DateTimeOffset Received { get; set; } = DateTimeOffset.UtcNow;
}

public class BillboardService : Billboard.Board.BoardBase
{
    public override async Task ShowMessage(IAsyncStreamReader<MessageRequest> requestStream, IServerStreamWriter<MessageReply> responseStream, ServerCallContext context)
    {
        using var tokenSource = new CancellationTokenSource();
        CancellationToken token = tokenSource.Token;

        await foreach (var request in requestStream.ReadAllAsync(token))
        {
            await responseStream.WriteAsync(new MessageReply { Pong = "pong" });
            await Task.Delay(request.DelayTime);
        }
    }
}
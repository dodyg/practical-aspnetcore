using Grpc.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Billboard;

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
    public override async Task<MessageReply> ShowMessage(IAsyncStreamReader<MessageRequest> requestStream, ServerCallContext context)
    {
        var fortunes = new List<ReceivedFortune>();

        using var tokenSource = new CancellationTokenSource();
        CancellationToken token = tokenSource.Token;

        await foreach (var request in requestStream.ReadAllAsync(token))
        {
            var inBed = request.FortuneCookie[0..^1] + " in bed.";

            fortunes.Add(new ReceivedFortune { Message = inBed });
        }

        var reply = new MessageReply();

        foreach (var f in fortunes)
        {
            reply.Fortunes.Add(new TranslatedFortune
            {
                Message = f.Message,
                ReceivedTime = f.Received.Ticks
            });
        }

        return reply;
    }
}
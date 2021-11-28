using Grpc.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Text;

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

public class BillboardService : Billboard.Board.BoardBase
{
    public override Task<Billboard.MessageReply> ShowMessage(Billboard.MessageRequest request, ServerCallContext context)
    {
        var message = new StringBuilder();
        message.AppendLine($"WageCase {request.WageCase}");
        message.AppendLine($"WageValue {request.WageValue}");
        message.AppendLine($"BonusCase {request.BonusCase}");
        if (request.BonusCase == Billboard.MessageRequest.BonusOneofCase.None)
        {
            message.AppendLine("BonusCase None means that BonusValue property is never set by the requester. So you can ignore whatever values you see at BonusValue property.");
        }
        message.AppendLine($"BonusValue {request.BonusValue}");

        var now = DateTime.UtcNow;
        return Task.FromResult(new Billboard.MessageReply
        {
            ReceivedTime = now.Ticks,
            Message = message.ToString()
        });
    }
}
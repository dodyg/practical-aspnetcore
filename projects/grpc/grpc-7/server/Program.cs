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
        foreach (var (k, c) in request.Capabilities)
        {
            message.AppendLine($"'{k}' = {c.Name} level {c.Level}");
        }

        var now = DateTime.UtcNow;
        return Task.FromResult(new Billboard.MessageReply
        {
            ReceivedTime = now.Ticks,
            Message = message.ToString()
        });
    }
}
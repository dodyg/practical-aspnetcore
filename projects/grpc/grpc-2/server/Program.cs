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

public class BillboardService : Billboard.Board.BoardBase
{
    public override async Task ShowMessage(MessageRequest request, IServerStreamWriter<MessageReply> responseStream, ServerCallContext context)
    {
        foreach (var x in Enumerable.Range(1, 10))
        {
            var now = DateTime.UtcNow;

            await responseStream.WriteAsync(new Billboard.MessageReply
            {
                DisplayTime = now.Ticks,
                Message = $"Hello {request.Name}"
            });

            await Task.Delay(5000);
        }
    }
}

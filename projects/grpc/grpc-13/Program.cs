using Grpc.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Grpc.JsonTranscoding;

var builder = WebApplication.CreateBuilder();
builder.Services.AddGrpc().AddJsonTranscoding();
builder.WebHost.ConfigureKestrel(k =>
{
    k.ConfigureEndpointDefaults(options => options.Protocols = HttpProtocols.Http2);
    k.ListenLocalhost(5500, o => o.UseHttps());
});

var app = builder.Build();

app.MapGrpcService<BillboardService>();
app.MapGet("/", () => Results.Content("""
<html>
<body>

<a href="/v1/message/{sender}">The endpoint</a>
<p>
sender:<br/>
<span id="sender"/>
</p>
<p>
time: <br/>
<span id="displayTime" />
</p>

<script>
function convertInJs(input){
    let epochTicks = 621355968000000000,    // the number of .net ticks at the unix epoch
        ticksPerMillisecond = 10000,        // there are 10000 .net ticks per millisecond
        jsTicks = 0;                        // ticks in javascript environment

    jsTicks = (input - epochTicks) / ticksPerMillisecond;

    const jsDate = new Date(jsTicks); // N.B. Js applies local timezone in automatic

    return jsDate
}

    async function grpc(){
        const result = await fetch('https://localhost:5500/v1/message/anne');
        const response = await result.json();
        return response
    }

    grpc().then( result => { 
        document.getElementById("sender").innerHTML = result.receiveFrom;
        document.getElementById("displayTime").innerHTML = convertInJs(result.displayTime);
        console.log(result);
    });
</script>
</body>
</html>
""", "text/html"));

app.Run();

public class BillboardService : Billboard.Board.BoardBase
{
    public override Task<Billboard.MessageReply> ShowMessage(Billboard.MessageRequest request, ServerCallContext context)
    {
        var now = DateTime.UtcNow;
        return Task.FromResult(new Billboard.MessageReply
        {
            DisplayTime = now.Ticks,
            ReceiveFrom = request.Sender
        });
    }
}
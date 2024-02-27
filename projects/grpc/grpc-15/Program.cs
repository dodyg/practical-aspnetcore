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
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-0evHe/X+R7YkIZDRvuzKMRqM+OrBnVFBL6DOitfPri4tjfHxaWutUpFmBp4vmVor" crossorigin="anonymous">
<body>

<div class="container">
    <h1>gRPC JSON Transcoding POST</h1>
    <div class="row mb-3">
        <div class="col-md-3">
            <form>
                <input type="text" class="form-control" id="senderInput" placeholder="Please enter sender" /><br/>
                <input type="text" class="form-control" id="cityInput" placeholder="Please enter city" /><br/>
                <input type="number"  min="18" step="1" class="form-control" id="ageInput" placeholder="Please enter age" /><br/>
                <button type="button" id="postBtn" class="btn btn-primary">Send</button>
            </form>
        </div>
    </div>
    <div class="card">
        <div class="card-body">
            <p>
            sender:<br/>
            <span id="sender"/>
            </p>
            <p>
            city:<br/>
            <span id="city"/>
            </p>
            <p>
            sender:<br/>
            <span id="age"/>
            </p>
            <p>
            time: <br/>
            <span id="displayTime" />
            </p>
        </div>
    </div>
</div>
<script>
    const postBtn = document.getElementById("postBtn");
    postBtn.addEventListener("click", function() {
        const senderInput = document.getElementById("senderInput");
        const cityInput = document.getElementById("cityInput");
        const ageInput = document.getElementById("ageInput");

        const input = { sender: senderInput.value, city: cityInput.value, age: parseInt(ageInput.value) };
        grpc(input).then( result => { 
            document.getElementById("sender").innerHTML = result.receivedFrom;
            document.getElementById("city").innerHTML = result.receivedCity;
            document.getElementById("age").innerHTML = result.receivedAge;
            document.getElementById("displayTime").innerHTML = convertInJs(result.displayTime);
            console.log(result);
        });
    });

    function convertInJs(input){
        let epochTicks = 621355968000000000,    // the number of .net ticks at the unix epoch
            ticksPerMillisecond = 10000,        // there are 10000 .net ticks per millisecond
            jsTicks = 0;                        // ticks in javascript environment

        jsTicks = (input - epochTicks) / ticksPerMillisecond;

        const jsDate = new Date(jsTicks); // N.B. Js applies local timezone in automatic

        return jsDate
    }

    async function grpc(input){
        const payload = {
            city: input.city,
            age: input.age
        };

        console.log("Payload ", payload);

        const result = await fetch(`https://localhost:5500/v1/message/${input.sender}`, {
            headers: {
                'Content-Type' : 'application/json'
            },
            body: JSON.stringify(payload),
            method: 'POST'
        });
        const response = await result.json();
        return response
    }
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
            ReceivedFrom = request.Sender,
            ReceivedAge = request.Age,
            ReceivedCity = request.City
        });
    }
}
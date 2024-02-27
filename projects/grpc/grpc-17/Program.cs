using Grpc.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder();
builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddSingleton<Accumulator>();
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
    <h1>gRPC JSON Transcoding PATCH</h1>
    <div class="row mb-3">
        <div class="col-md-3">
            <form>
                <input type="number"  min="18" step="1" class="form-control" id="numberInput" placeholder="Please enter a number" /><br/>
                <button type="button" id="postBtn" class="btn btn-primary">Send</button>
            </form>
        </div>
    </div>
    <div class="card">
        <div class="card-body">
            <p>
            total:<br/>
            <span id="total"/>
            </p>
        </div>
    </div>
</div>
<script>
    const postBtn = document.getElementById("postBtn");
    postBtn.addEventListener("click", function() {
        const numberInput = document.getElementById("numberInput");

        const input = { number : parseInt(numberInput.value) };
        grpc(input).then( result => { 
            document.getElementById("total").innerHTML = result.totalNumber;
        });
    });

    async function grpc(input){
        const payload = {
            addNumber: input.number
        };

        console.log("Payload ", payload);

        const result = await fetch(`https://localhost:5500/v1/message`, {
            headers: {
                'Content-Type' : 'application/json'
            },
            body: JSON.stringify(payload),
            method: 'PATCH'
        });
        const response = await result.json();
        return response
    }
</script>
</body>
</html>
""", "text/html"));

app.Run();

public class Accumulator
{
    public int Total { get; set;}

    public void Add(int number) => Total += number;
}

public class BillboardService : Billboard.Board.BoardBase
{
    Accumulator _acc;

    public BillboardService(Accumulator acc) 
    {
        _acc = acc;
    }
    
    public override Task<Billboard.MessageReply> ShowMessage(Billboard.MessageRequest request, ServerCallContext context)
    {
        _acc.Add(request.AddNumber);

        return Task.FromResult(new Billboard.MessageReply
        {
            TotalNumber = _acc.Total
        });
    }
}
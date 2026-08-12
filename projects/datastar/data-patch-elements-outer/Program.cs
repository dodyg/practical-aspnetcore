using System.Text.Json;
using System.Text.Json.Nodes;
using System.Linq;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder();

var app = builder.Build();

app.MapGet("/sse", (HttpRequest req, CancellationToken cancellationToken) =>
{
    async IAsyncEnumerable<string> GreetingAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        yield return $"""elements <div id="mode-outer">mode outer</div>""";

        await Task.CompletedTask;
    }

    if (req.Headers["Datastar-Request"] != "true")
        return Results.BadRequest("Datastar request header is missing.");

    return Results.ServerSentEvents(GreetingAsync(cancellationToken), "datastar-patch-elements");
});

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync($$"""
    <html>
        <head>
          <script type="module" src="https://cdn.jsdelivr.net/gh/starfederation/datastar@v1.0.2/bundles/datastar.js"></script>
          <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        <body class="container">
            <h1 data-init="@get('/sse')">mode outer</h1>
            <div class="grid">
                <div id="mode-outer" style="border:1px solid red;padding:5px;">Loading...</div>
            </div>
            <div class="grid">
                <div id="mode-inner" style="border:1px solid red;padding:5px;">Loading...</div>
            </div>
        </body>
    </html>
    """);
});
 
app.Run();

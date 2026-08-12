using System.Text.Json;
using System.Text.Json.Nodes;
using System.Linq;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder();

var app = builder.Build();

app.MapGet("/sse", (HttpRequest req, CancellationToken cancellationToken) =>
{
    async IAsyncEnumerable<string> GreetingAsync(JsonNode node, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var elementIds = node.AsObject().Select(x => x.Key).ToArray();

        foreach (var elementId in elementIds)
            yield return $"""elements <div id="{elementId}">{node[elementId]} {DateTime.Now}</div>""";

        await Task.CompletedTask;
    }

    if (req.Headers["Datastar-Request"] != "true")
        return Results.BadRequest("Datastar request header is missing.");

    var data = JsonNode.Parse(req.Query["datastar"]);

    return Results.ServerSentEvents(GreetingAsync(data,  cancellationToken), "datastar-patch-elements");
});

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync($$"""
    <html>
        <head>
          <script type="module" src="https://cdn.jsdelivr.net/gh/starfederation/datastar@v1.0.2/bundles/datastar.js"></script>
          <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        <body class="container" data-signals="{greeting:'hello world', greeting2:'greetings earthlings'}">
            <h1 data-init="@get('/sse')">Datastar SSE Hello World</h1>
            <div class="grid">
                <div id="greeting">Loading...</div>
                <div id="greeting2">Loading...</div>
            </div>
        </body>
    </html>
    """);
});
 
app.Run();

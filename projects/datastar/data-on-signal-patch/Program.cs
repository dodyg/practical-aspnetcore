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

        await Task.Delay(3000, cancellationToken); // Simulate some delay

        foreach (var elementId in elementIds.Where(x => !x.Equals("showMessage", StringComparison.OrdinalIgnoreCase)))
        {
            yield return $$"""signals { {{elementId}}: 'hello world from the backend with signal {{elementId}}' }""";
        }
    }

    if (req.Headers["Datastar-Request"] != "true")
        return Results.BadRequest("Datastar request header is missing.");

    var data = JsonNode.Parse(req.Query["datastar"]);

    return Results.ServerSentEvents(GreetingAsync(data, cancellationToken), "datastar-patch-signals");
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
            <h1 data-init__delay.100ms="@get('/sse')" data-on-signal-patch="alert(JSON.stringify(patch))">data-on-signal-patch</h1>
            <div class="grid">
                <div data-text="$greeting"></div>
                <div data-text="$greeting2"></div>
            </div>
            <br/>
            <h3>All signals on this page</h3>
            <pre data-json-signals></pre>
        </body>
    </html>
    """);
});
 
app.Run();

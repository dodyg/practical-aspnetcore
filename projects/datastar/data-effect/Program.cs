var builder = WebApplication.CreateBuilder();

var app = builder.Build();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync($$"""
    <html>
        <head>
          <script type="module" src="https://cdn.jsdelivr.net/gh/starfederation/datastar@v1.0.2/bundles/datastar.js"></script>
          <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        <body class="container" data-signals="{ first : 1, second : 1, total : 2}">
            <h1 data-effect = "$total = $first + $second">data-effect</h1>
            <div data-text="$total"></div>
            <div data-effect = "$total > 10 ? alert('over 10') : $first ;"></div>
            <br />
            <div class="grid">
                <div><button data-on:click="$second++">Increment</button></div>
            </div>
            <br/>
            <h3>All signals on this page</h3>
            <pre data-json-signals></pre>
        </body>
    </html>
    """);
});
 
app.Run();

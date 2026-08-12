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
        <body data-style:background-color="$bgColor" data-signals="{ bgColor : `white`, hiding : false, headColor : 'green' }">
            <main class="container">
                <h1 data-style="{ display: $hiding ? 'none' : 'flex', 'background-color': $headColor }">data-style</h1>
                <div class="grid">
                    <div><button data-on:click="$bgColor = $bgColor  == 'white' ? 'blue' : 'white';">Background Color</button></div>
                    <div><button data-on:click="$hiding = !$hiding" data-text="$hiding ? 'show' : 'hide'"></button></div>
                </div>
                <br/>
                <h3>All signals on this page</h3>
                <pre data-json-signals></pre>
            </main>
        </body>
    </html>
    """);
});
 
app.Run();

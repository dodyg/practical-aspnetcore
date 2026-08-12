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
        <body data-class:container="$container" data-class:container-fluid="$containerFluid" data-signals="{ container : true, containerFluid : false }">
            <h1>data-class</h1>
            <div class="grid">
                <div><button data-on:click="$container = $container  ? false : true; $containerFluid = !$container;">Change Layout</button></div>
            </div>
            <br/>
            <h3>All signals on this page</h3>
            <pre data-json-signals></pre>
        </body>
    </html>
    """);
});
 
app.Run();

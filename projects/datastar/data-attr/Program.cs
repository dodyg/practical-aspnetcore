using System.Text.Json;
using System.Text.Json.Nodes;
using System.Linq;
using System.Runtime.CompilerServices;

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
        <body class="container" data-signals="{btn1 : 'Normal Button', btn2 : true}">
            <h1 data-attr:title="'Data Atttribute Page'">data-atttr</h1>
            <div class="grid">
                <div><button data-text="$btn1"></button></div>
                <div><button data-attr:disabled="$btn2">Disabled Button</button></div>
                <div data-attr="{ width: '50px',  height:'50px', style:'border:1px solid red;' }"></div>
            </div>
            <br/>
            <h3>All signals on this page</h3>
            <pre data-json-signals></pre>
        </body>
    </html>
    """);
});
 
app.Run();

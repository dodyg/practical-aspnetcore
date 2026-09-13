using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using System.Text;

var builder = WebApplication.CreateBuilder();
builder.Services.AddAntiforgery();
var app = builder.Build();
app.UseAntiforgery();

app.MapGet("/", (HttpContext context, [FromServices] IAntiforgery anti) =>
{
    var token = anti.GetAndStoreTokens(context);
    return Results.Content($$"""
        <!DOCTYPE html>
        <html>
            <head>
                <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
                <meta name="htmx-config" content='{ "antiForgery": {"headerName":"{{token.HeaderName}}", "requestToken":"{{token.RequestToken}}"} }'>
            </head>
            <body class="container">
                <h1>Per-status response behavior</h1>
                <form hx-post="/validate" hx-target="#result" hx-status:422="swap:innerHTML target:#errors select:#validation-errors">
                    <button>Return 422</button>
                </form>
                <br/>
                <button hx-post="/server-error" hx-target="#result" hx-status:5xx="swap:innerHTML push:false">Return 500</button>
                
                <div id="result">The default target.</div>
                <div id="errors">422 output goes here.</div>
            
                <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
                <script>
                    document.addEventListener("htmx:config:request", e => { 
                    if (e.detail.ctx.request.method !== "GET") 
                        e.detail.ctx.request.headers[htmx.config.antiForgery.headerName] = htmx.config.antiForgery.requestToken; 
                    });
                </script>
            </body>
            </html>

        """, "text/html");
});

app.MapPost("/validate", async (HttpContext context, [FromServices] IAntiforgery anti) =>
{
    await anti.ValidateRequestAsync(context);
    return Results.Content("<div id='validation-errors'>422: name is required</div>", "text/html", statusCode: 422);
});

app.MapPost("/server-error", async (HttpContext context, [FromServices] IAntiforgery anti) =>
{
    await anti.ValidateRequestAsync(context);
    return Results.Content("<div>500: server failure</div>", "text/html", statusCode:500);
});

app.Run();

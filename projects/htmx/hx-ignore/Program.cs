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
                <meta name="htmx-config" content='{ "antiForgery": {"headerName":"{{token.HeaderName}}", "requestToken":"{{token.RequestToken}}"} }'>
                <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
            </head>
            <body class="container">
                <h1>Ignoring and disabling</h1>
                <div hx-ignore style="border:1px solid #999;padding:1rem">
                    <p>Nothing below this boundary is processed by htmx.</p>
                    <button hx-get="/ignored" hx-target="#ignored-result">This button does nothing</button>
                </div>
            
                <p id="ignored-result">Ignored result remains unchanged.</p>
            
                <button hx-post="/slow" hx-disable hx-target="#slow-result">Disable me during a slow request</button>
                
                <p id="slow-result">Ready.</p>
            
                <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
                <script>document.addEventListener("htmx:config:request", e => { if (e.detail.ctx.request.method !== "GET") e.detail.ctx.request.headers[htmx.config.antiForgery.headerName] = htmx.config.antiForgery.requestToken; });</script>
            </body>
            </html>

        """, "text/html", Encoding.UTF8);
});

app.MapPost("/slow", async (HttpContext context, [FromServices] IAntiforgery anti) =>
{
    await anti.ValidateRequestAsync(context);
    await Task.Delay(1500);
    return Results.Content("Request finished.");
});

app.Run();

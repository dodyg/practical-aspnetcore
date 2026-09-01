using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddAntiforgery();
var app = builder.Build();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/", (HttpContext context, [FromServices] IAntiforgery anti) =>
{
    var token = anti.GetAndStoreTokens(context);

    var html = $$"""
        <!DOCTYPE html>
        <html>
            <head>
                <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
                <style>
                    li{
                        cursor:pointer;
                    }

                    .htmx-indicator{
                        opacity:0;
                        visibility:hidden;
                        transition: opacity 500ms ease-in;
                        position:fixed;
                        left:50%;
                        top:50%;
                        transform:translate(-50%, -50%);
                        z-index:1000;
                        display:flex;
                        align-items:center;
                        gap:0.5rem;
                        padding:1rem;
                        background:grey;
                    }
                    
                    .htmx-request .htmx-indicator{
                        opacity:1;
                        visibility:visible;
                    }
                    
                    .htmx-request.htmx-indicator{
                        opacity:1;
                        visibility:visible;
                    }
                </style>
                <meta name="htmx-config" content='{ "includeIndicatorCSS": false, "antiForgery": {"headerName" : "{{token.HeaderName}}", "requestToken" : "{{token.RequestToken}}" } }'>
            </head>
            <body class="container">
            <h1>hx-spinner</h1>
            <p>Click on the below links to see request spinner and the response</p>
            <p>The sample supplies its own indicator CSS and disables htmx 4's generated stylesheet.</p>
            <ul hx-indicator:inherited="#spinner">
                <li hx-get="/htmx" >GET</li>
                <li hx-post="/htmx">POST</li>
                <li hx-put="/htmx">PUT</li>
                <li hx-patch="/htmx">PATCH</li>
                <li hx-delete="/htmx">DELETE</li>
            </ul>
            <div id="spinner" class="htmx-indicator" role="status" aria-live="polite">
                <img src="/90-ring.svg" width="90" height="90" alt=""/>
                <span>Loading...</span>
            </div>
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
            <script>
                document.addEventListener("htmx:config:request", (evt) => {
                    let httpVerb = evt.detail.ctx.request.method.toUpperCase();
                    if (httpVerb === 'GET') return;
                    
                    let antiForgery = htmx.config.antiForgery;
                    if (antiForgery) {
                        // already specified on form, short circuit
                        if (evt.detail.ctx.request.body.has(antiForgery.formFieldName))
                            return;
                        
                        if (antiForgery.headerName) {
                            evt.detail.ctx.request.headers[antiForgery.headerName] = antiForgery.requestToken;

                        } else {
                            evt.detail.ctx.request.body.set(antiForgery.formFieldName, antiForgery.requestToken);

                        }
                    }
                });
            </script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

var htmx = app.MapGroup("/htmx").AddEndpointFilter(async (context, next) =>
{
    if (context.HttpContext.Request.Headers.ContainsKey("HX-Request") is false)
        return Results.Content("");

    if (context.HttpContext.Request.Method == "GET")
        return await next(context);

    await context.HttpContext.RequestServices.GetRequiredService<IAntiforgery>()!.ValidateRequestAsync(context.HttpContext);
    return await next(context);
});

htmx.MapGet("/", async (HttpRequest request) =>
{
    await Task.Delay(5000);
    return Results.Content($"GET => {DateTime.UtcNow}");
});

htmx.MapPost("/", async (HttpRequest request) =>
{
    await Task.Delay(5000);
    return Results.Content($"POST => {DateTime.UtcNow}");
});

htmx.MapDelete("/", async (HttpRequest request) =>
{
    await Task.Delay(5000);
    return Results.Content($"DELETE => {DateTime.UtcNow}");
});

htmx.MapPut("/", async (HttpRequest request) =>
{
    await Task.Delay(5000);
    return Results.Content($"PUT => {DateTime.UtcNow}");
});

htmx.MapPatch("/", async (HttpRequest request) =>
{
    await Task.Delay(5000);
    return Results.Content($"PATCH => {DateTime.UtcNow}");
});

app.Run();

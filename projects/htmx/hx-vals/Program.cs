using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddAntiforgery();
var app = builder.Build();

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
                </style>
                <meta name="htmx-config" content='{ "antiForgery": {"headerName" : "{{ token.HeaderName}}", "requestToken" : "{{token.RequestToken }}" } }'>
            </head>
            <body class="container">
            <h1>Passing parameters to all HTTP verbs via hx-vals</h1>
            <p>Click on the below links to see the response</p>
            <ul>
                <li hx-get="/htmx" hx-vals='{"Name": "Anna"}'>GET</li>
                <li hx-post="/htmx" hx-vals='{"Name": "Anna"}'>POST</li>
                <li hx-put="/htmx" hx-vals='{"Name": "Anna"}'>PUT</li>
                <li hx-patch="/htmx" hx-vals='{"Name": "Anna"}'>PATCH</li>
                <li hx-delete="/htmx" hx-vals='{"Name": "Anna"}'>DELETE</li>
            </ul>
            <h2>Dynamic values with the htmx 4 <code>js:</code> prefix</h2>
            <p>Each request evaluates <code>eventDetail()</code> at trigger time.</p>
            <ul>
                <li hx-get="/htmx" hx-vals="js:{ count: eventDetail() }">GET dynamic</li>
                <li hx-post="/htmx" hx-vals="js:{ count: eventDetail() }">POST dynamic</li>
                <li hx-put="/htmx" hx-vals="js:{ count: eventDetail() }">PUT dynamic</li>
                <li hx-patch="/htmx" hx-vals="js:{ count: eventDetail() }">PATCH dynamic</li>
                <li hx-delete="/htmx" hx-vals="js:{ count: eventDetail() }">DELETE dynamic</li>
            </ul>
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
            <script>
                function eventDetail() { return new Date().toLocaleTimeString(); }

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

htmx.MapGet("/", (HttpRequest request) =>
{
    return Results.Content($"GET => {DateTime.UtcNow} + {request.Query["Name"]} + count={request.Query["count"]}");
});

htmx.MapPost("/", (HttpRequest request) =>
{
    return Results.Content($"POST => {DateTime.UtcNow} + {request.Form["Name"]} + count={request.Form["count"]}");
});

htmx.MapDelete("/", (HttpRequest request) =>
{
    return Results.Content($"DELETE => {DateTime.UtcNow} + {request.Query["Name"]} + count={request.Query["count"]}");
});

htmx.MapPut("/", (HttpRequest request) =>
{
    return Results.Content($"PUT => {DateTime.UtcNow} + {request.Form["Name"]} + count={request.Form["count"]}");
});

htmx.MapPatch("/", (HttpRequest request) =>
{
    return Results.Content($"PATCH => {DateTime.UtcNow} + {request.Form["Name"]} + count={request.Form["count"]}");
});

app.Run();



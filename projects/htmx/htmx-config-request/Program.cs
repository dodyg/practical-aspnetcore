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
                <style>
                    li{
                        cursor:pointer;
                    }
                </style>
                <meta name="htmx-config" content='{ "antiForgery": {"headerName" : "{{ token.HeaderName}}", "requestToken" : "{{token.RequestToken }}" } }'>
            </head>
            <body>
            <h1>Passing parameters via htmx:config:request</h1>
            <p>Click on the below links to see the response</p>
            <ul>
                <li hx-get="/htmx">GET</li>
                <li hx-post="/htmx">POST</li>
                <li hx-put="/htmx">PUT</li>
                <li hx-patch="/htmx">PATCH</li>
                <li hx-delete="/htmx"'>DELETE</li>
            </ul>
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
            <script>
                document.addEventListener("htmx:config:request", (evt) => {
                    evt.detail.ctx.request.body.set("Name", "John Doe");
                    
                    // This is for the anti-forgery token
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
    if (context.HttpContext.Request.Method == "GET")
        return await next(context);

    await context.HttpContext.RequestServices.GetRequiredService<IAntiforgery>()!.ValidateRequestAsync(context.HttpContext);
    return await next(context);
});

htmx.MapGet("/", (HttpRequest request) =>
{
    if (request.Headers.ContainsKey("HX-Request") is false)
        return Results.Content("");

    return Results.Content($"GET => {DateTime.UtcNow} + {request.Query["Name"]}");
});

htmx.MapPost("/", (HttpRequest request) =>
{
    if (request.Headers.ContainsKey("HX-Request") is false)
        return Results.Content("");

    return Results.Content($"POST => {DateTime.UtcNow} + {request.Form["Name"]}");
});

htmx.MapDelete("/", (HttpRequest request) =>
{
    if (request.Headers.ContainsKey("HX-Request") is false)
        return Results.Content("");

    return Results.Content($"DELETE => {DateTime.UtcNow} + {request.Query["Name"]}");
});

htmx.MapPut("/", (HttpRequest request) =>
{
    if (request.Headers.ContainsKey("HX-Request") is false)
        return Results.Content("");

    return Results.Content($"PUT => {DateTime.UtcNow} + {request.Form["Name"]}");
});

htmx.MapPatch("/", (HttpRequest request) =>
{
    if (request.Headers.ContainsKey("HX-Request") is false)
        return Results.Content("");

    return Results.Content($"PATCH => {DateTime.UtcNow} + {request.Form["Name"]}");
});

app.Run();



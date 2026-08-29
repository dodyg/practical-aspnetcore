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
                <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
            </head>
            <body class="container">
            <h1>hx-replace-url</h1>
            <p>Click on the below links to see the response and check the url change at the browser top bar</p>
            <div class="row">
                <div class="col-md-6">
                    <h2>With hx-replace-url="true"</h2>
                    <ul>
                        <li hx-get="/htmx/get" hx-replace-url="true">GET</li>
                        <li hx-post="/htmx/post" hx-replace-url="true">POST</li>
                        <li hx-put="/htmx/put" hx-replace-url="true">PUT</li>
                        <li hx-patch="/htmx/patch" hx-replace-url="true">PATCH</li>
                        <li hx-delete="/htmx/delete" hx-replace-url="true">DELETE</li>
                    </ul>
                </div>
                <div class="col-md-6">
                    <h2>With hx-replace-url="{other url}"</h2>
                    <ul>
                        <li hx-get="/htmx/get" hx-replace-url="/person/anna">GET</li>
                        <li hx-post="/htmx/post" hx-replace-url="/person/john">POST</li>
                        <li hx-put="/htmx/put" hx-replace-url="/person/ahmed">PUT</li>
                        <li hx-patch="/htmx/patch" hx-replace-url="/person/gaby">PATCH</li>
                        <li hx-delete="/htmx/delete" hx-replace-url="/person/daniela">DELETE</li>
                    </ul>
                </div>
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

htmx.MapGet("/get", (HttpRequest request) =>
{
    return Results.Content($"GET => {DateTime.UtcNow}");
});

htmx.MapPost("/post", (HttpRequest request) =>
{
    return Results.Content($"POST => {DateTime.UtcNow}");
});

htmx.MapDelete("/delete", (HttpRequest request) =>
{
    return Results.Content($"DELETE => {DateTime.UtcNow}");
});

htmx.MapPut("/put", (HttpRequest request) =>
{
    return Results.Content($"PUT => {DateTime.UtcNow}");
});

htmx.MapPatch("/patch", (HttpRequest request) =>
{
    return Results.Content($"PATCH => {DateTime.UtcNow}");
});

app.Run();

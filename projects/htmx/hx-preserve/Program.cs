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
            <h1>hx-preserve</h1>
            <p>Click on the below links to see the response</p>
            <ul>
                <li hx-get="/htmx">
                    <div id="get" hx-preserve="true">GET Preserved</div>
                </li>
                <li hx-post="/htmx">
                    <div id="post" hx-preserve="true">POST Preserved</div>
                </li>
                <li hx-put="/htmx">
                    <div id="put" hx-preserve="true">PUT Preserved</div>
                </li>
                <li hx-patch="/htmx">
                    <div id="patch" hx-preserve="true">PATCH Preserved</div>
                </li>
                <li hx-delete="/htmx">
                    <div id="delete" hx-preserve="true">DELETE Preserved</div>
                </li>
            </ul>
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

htmx.MapGet("/", (HttpRequest request) =>
{
    return Results.Content($"""<div id="get" hx-preserve="true"></div> GET => {DateTime.UtcNow}""");
});

htmx.MapPost("/", (HttpRequest request) =>
{
    return Results.Content($"""<div id="post" hx-preserve="true"></div> POST => {DateTime.UtcNow}""");
});

htmx.MapDelete("/", (HttpRequest request) =>
{
    return Results.Content($"""<div id="delete" hx-preserve="true"></div> DELETE => {DateTime.UtcNow}""");
});

htmx.MapPut("/", (HttpRequest request) =>
{
    return Results.Content($"""<div id="put" hx-preserve="true"></div> PUT => {DateTime.UtcNow}""");
});

htmx.MapPatch("/", (HttpRequest request) =>
{
    return Results.Content($"""<div id="patch" hx-preserve="true"></div> PATCH => {DateTime.UtcNow}""");
});

app.Run();

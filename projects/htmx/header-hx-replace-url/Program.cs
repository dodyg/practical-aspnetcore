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
            <h1>HX-Replace-Url</h1>
            <p>Click on the below links to see the response. Don't forget to check your browser url.</p>
            <ul>
                <li hx-get="/htmx">GET</li>
                <li hx-post="/htmx">POST</li>
                <li hx-put="/htmx">PUT</li>
                <li hx-patch="/htmx">PATCH</li>
                <li hx-delete="/htmx">DELETE</li>
            </ul>
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js></script>
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

htmx.MapGet("/", (HttpRequest request, HttpResponse response) =>
{
response.Headers.Append("HX-Replace-Url", "/get");

    return Results.Content($"GET => {DateTime.UtcNow}");
});

htmx.MapPost("/", (HttpRequest request, HttpResponse response) =>
{
response.Headers.Append("HX-Replace-Url", "/post");

    return Results.Content($"POST => {DateTime.UtcNow}");
});

htmx.MapDelete("/", (HttpRequest request, HttpResponse response) =>
{
response.Headers.Append("HX-Replace-Url", "/delete");

    return Results.Content($"DELETE => {DateTime.UtcNow}");
});

htmx.MapPut("/", (HttpRequest request, HttpResponse response) =>
{
response.Headers.Append("HX-Replace-Url", "/put");
    
    return Results.Content($"PUT => {DateTime.UtcNow}");
});

htmx.MapPatch("/", (HttpRequest request, HttpResponse response) =>
{   
response.Headers.Append("HX-Replace-Url", "/patch");

    return Results.Content($"PATCH => {DateTime.UtcNow}");
});

app.Run();

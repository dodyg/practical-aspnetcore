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
            <h1>Push URL to browser history</h1>
            <p>Click on the links below to see the URL change in the browser address bar.</p>
            <ul hx-push-url="true">
                <li hx-get="/htmx/get">GET</li>
                <li hx-post="/htmx/post">POST</li>
                <li hx-put="/htmx/put">PUT</li>
                <li hx-patch="/htmx/patch">PATCH</li>
                <li hx-delete="/htmx/delete">DELETE</li>
            </ul>
            <script src="https://unpkg.com/htmx.org@2.0.0" integrity="sha384-wS5l5IKJBvK6sPTKa2WZ1js3d947pvWXbPJ1OmWfEuxLgeHcEbjUUA5i9V5ZkpCw" crossorigin="anonymous"></script>
            <script>
                document.addEventListener("htmx:configRequest", (evt) => {
                    let httpVerb = evt.detail.verb.toUpperCase();
                    if (httpVerb === 'GET') return;
                    
                    let antiForgery = htmx.config.antiForgery;
                    if (antiForgery) {
                        // already specified on form, short circuit
                        if (evt.detail.parameters[antiForgery.formFieldName])
                            return;
                        
                        if (antiForgery.headerName) {
                            evt.detail.headers[antiForgery.headerName]
                                = antiForgery.requestToken;
                        } else {
                            evt.detail.parameters[antiForgery.formFieldName]
                                = antiForgery.requestToken;
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

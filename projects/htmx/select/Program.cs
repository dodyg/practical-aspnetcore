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
            <body>
            <div class="container">
            <h1>Select element from the server response</h1>
            <p>Click on the below links to see the response</p>
            
            <div class="row mt-5">
                <div class="col-md-6">
                    <h2>Without hx-select</h2>
                    <ul>
                        <li hx-get="/htmx">GET</li>
                        <li hx-post="/htmx">POST</li>
                        <li hx-put="/htmx">PUT</li>
                        <li hx-patch="/htmx">PATCH</li>
                        <li hx-delete="/htmx">DELETE</li>
                    </ul>
                </div>
                <div class="col-md-6">
                    <h2>With hx-select</h2>
                    <ul hx-select="#result">
                        <li hx-get="/htmx">GET</li>
                        <li hx-post="/htmx">POST</li>
                        <li hx-put="/htmx">PUT</li>
                        <li hx-patch="/htmx">PATCH</li>
                        <li hx-delete="/htmx">DELETE</li>
                    </ul>
                </div>
            </div>
            </div>
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

htmx.MapGet("/", (HttpRequest request) =>
{
    return Results.Content($"""
    This is the response 
    <div id="result">
    GET => {DateTime.UtcNow}
    </div>
    """);
});

htmx.MapPost("/", (HttpRequest request) =>
{
    return Results.Content($"""
    This is the response 
    <div id="result">
    POST => {DateTime.UtcNow}
    </div>
    """);
});

htmx.MapDelete("/", (HttpRequest request) =>
{
    return Results.Content($"""
    This is the response 
    <div id="result">
    DELETE => {DateTime.UtcNow}
    </div>
    """);
});

htmx.MapPut("/", (HttpRequest request) =>
{
    return Results.Content($"""
    This is the response 
    <div id="result">
    PUT => {DateTime.UtcNow}
    </div>
    """);
});

htmx.MapPatch("/", (HttpRequest request) =>
{
    return Results.Content($"""
    This is the response 
    <div id="result">
    PATCH => {DateTime.UtcNow}
    </div>
    """);
});

app.Run();

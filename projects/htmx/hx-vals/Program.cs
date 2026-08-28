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
            <h1>Passing parameters to all HTTP verbs via hx-vals</h1>
            <p>Click on the below links to see the response</p>
            <ul>
                <li hx-get="/htmx" hx-vals='{"Name": "Anna"}'>GET</li>
                <li hx-post="/htmx" hx-vals='{"Name": "Anna"}'>POST</li>
                <li hx-put="/htmx" hx-vals='{"Name": "Anna"}'>PUT</li>
                <li hx-patch="/htmx" hx-vals='{"Name": "Anna"}'>PATCH</li>
                <li hx-delete="/htmx" hx-vals='{"Name": "Anna"}'>DELETE</li>
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

htmx.MapGet("/", (HttpRequest request) =>
{
    return Results.Content($"GET => {DateTime.UtcNow} + {request.Query["Name"]}");
});

htmx.MapPost("/", (HttpRequest request) =>
{
    return Results.Content($"POST => {DateTime.UtcNow} + {request.Form["Name"]}");
});

htmx.MapDelete("/", (HttpRequest request) =>
{
    return Results.Content($"DELETE => {DateTime.UtcNow} + {request.Query["Name"]}");
});

htmx.MapPut("/", (HttpRequest request) =>
{
    return Results.Content($"PUT => {DateTime.UtcNow} + {request.Form["Name"]}");
});

htmx.MapPatch("/", (HttpRequest request) =>
{
    return Results.Content($"PATCH => {DateTime.UtcNow} + {request.Form["Name"]}");
});

app.Run();



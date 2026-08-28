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

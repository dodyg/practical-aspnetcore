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
                <meta name="htmx-config" content='{ "antiForgery": {"headerName" : "{{token.HeaderName}}", "requestToken" : "{{token.RequestToken}}" } }'>
            </head>
            <body>
            <h1>HX-Reselect header</h1>
            <p>Click on the below links to see the response</p>
            <ul>
                <li hx-get="/htmx">GET</li>
                <li hx-post="/htmx">POST</li>
                <li hx-put="/htmx">PUT</li>
                <li hx-patch="/htmx">PATCH</li>
                <li hx-delete="/htmx">DELETE</li>
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

htmx.MapGet("/", (HttpRequest request, HttpResponse response) =>
{
response.Headers.Append("HX-Reselect", "#get");

    return Results.Content($"""
    GET => {DateTime.UtcNow}
        <div id="get">RESELECTED GET => {DateTime.UtcNow}</div>
    """);
});

htmx.MapPost("/", (HttpRequest request, HttpResponse response) =>
{
response.Headers.Append("HX-Reselect", "#post");

    return Results.Content($"""
        POST => {DateTime.UtcNow}
        <div id="post">RESELECTED POST => {DateTime.UtcNow}</div>
        """);
});

htmx.MapDelete("/", (HttpRequest request, HttpResponse response) =>
{
response.Headers.Append("HX-Reselect", "#delete");

    return Results.Content($"""
        DELETE => {DateTime.UtcNow}
        <div id="delete">RESELECTED DELETE => {DateTime.UtcNow}</div>
        """);
});

htmx.MapPut("/", (HttpRequest request, HttpResponse response) =>
{
response.Headers.Append("HX-Reselect", "#put");

    return Results.Content($"""
        PUT => {DateTime.UtcNow}
        <div id="put">RESELECTED PUT => {DateTime.UtcNow}</div>
        """);
});

htmx.MapPatch("/", (HttpRequest request, HttpResponse response) =>
{
response.Headers.Append("HX-Reselect", "#patch");

    return Results.Content($"""
        PATCH => {DateTime.UtcNow}
        <div id="patch">RESELECTED PATCH => {DateTime.UtcNow}</div>
        """);
});

app.Run();

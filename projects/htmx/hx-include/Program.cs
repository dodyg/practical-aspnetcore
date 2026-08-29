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
            <h1>Passing parameters to all HTTP verbs via hx-include targeting an input form</h1>
            <p>Click on the below links to see the response</p>
            <ul>
                <li hx-get="/htmx" hx-include="[name='Name']">GET</li>
                <li hx-post="/htmx" hx-include="[name='Name']">POST</li>
                <li hx-put="/htmx" hx-include="[name='Name']">PUT</li>
                <li hx-patch="/htmx" hx-include="[name='Name']">PATCH</li>
                <li hx-delete="/htmx" hx-include="[name='Name']">DELETE</li>
            </ul>
                <h2>Please fill this input</h2>
                <label for="Name">Name:</label></br>
                <input type="text" name="Name" id="Name"/> <br/>

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



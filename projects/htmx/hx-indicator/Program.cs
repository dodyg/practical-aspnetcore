using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddAntiforgery();
var app = builder.Build();

app.UseStaticFiles();
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

                    .htmx-indicator{
                        opacity:0;
                        transition: opacity 500ms ease-in;
                        position:absolute;
                        left:50%;
                        top:50%;
                    }
                    
                    .htmx-request .htmx-indicator{
                        opacity:1
                    }
                    
                    .htmx-request.htmx-indicator{
                        opacity:1
                    }
                </style>
                <meta name="htmx-config" content='{ "antiForgery": {"headerName" : "{{ token.HeaderName}}", "requestToken" : "{{token.RequestToken }}" } }'>
            </head>
            <body>
            <h1>hx-spinner</h1>
            <p>Click on the below links to see request spinner and the response</p>
            <ul hx-indicator="#spinner">
                <li hx-get="/htmx">GET</li>
                <li hx-post="/htmx">POST</li>
                <li hx-put="/htmx">PUT</li>
                <li hx-patch="/htmx">PATCH</li>
                <li hx-delete="/htmx">DELETE</li>
            </ul>
            <img  id="spinner" class="htmx-indicator" src="/90-ring.svg"/>
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

htmx.MapGet("/", async (HttpRequest request) =>
{
    await Task.Delay(5000);
    return Results.Content($"GET => {DateTime.UtcNow}");
});

htmx.MapPost("/", async (HttpRequest request) =>
{
    await Task.Delay(5000);
    return Results.Content($"POST => {DateTime.UtcNow}");
});

htmx.MapDelete("/", async (HttpRequest request) =>
{
    await Task.Delay(5000);
    return Results.Content($"DELETE => {DateTime.UtcNow}");
});

htmx.MapPut("/", async (HttpRequest request) =>
{
    await Task.Delay(5000);
    return Results.Content($"PUT => {DateTime.UtcNow}");
});

htmx.MapPatch("/", async (HttpRequest request) =>
{
    await Task.Delay(5000);
    return Results.Content($"PATCH => {DateTime.UtcNow}");
});

app.Run();

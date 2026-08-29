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
                    <h2>With hx-select</h2>
                    <ul hx-select:inherited="#result,#result2,.results">
                        <li hx-get="/htmx" hx-select-oob="#result-get">GET</li>
                        <li hx-post="/htmx" hx-select-oob="#result-post">POST</li>
                        <li hx-put="/htmx" hx-select-oob="#result-put">PUT</li>
                        <li hx-patch="/htmx" hx-select-oob="#result-patch">PATCH</li>
                        <li hx-delete="/htmx" hx-select-oob="#result-delete">DELETE</li>
                    </ul>
                </div>
                <div class="col-md-6">
                    <h2>Results</h2>
                    <h3>GET</h3>
                    <div id="result-get" class="mb-3"></div>
                    <hr/>
                    <h3>POST</h3>
                    <div id="result-post" class="mb-3"></div>
                    <hr/>
                    <h3>PUT</h3>
                    <div id="result-put" class="mb-3"></div>
                    <hr/>
                    <h3>PATCH</h3>
                    <div id="result-patch" class="mb-3"></div>
                    <hr/>
                    <h3>DELETE</h3>
                    <div id="result-delete" class="mb-3"></div>
                    <hr/>
                </div>
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

htmx.MapGet("/", (HttpRequest request) =>
{
    return Results.Content($"""
    This is the response 
    <div id="result">
    GET => {DateTime.UtcNow}
    </div>
    <div id="result2" class="mt-3" style="background-color:blue;color:white;">
    GET content via #result2
    </div>
    <div id="result-get">
        #result-get content
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
    <div class="results mt-3" style="background-color:blue;color:white;">
        POST content via .results class
    </div>
    <div class="results mt-3" style="background-color:green;color:white;">
        POST content via .results class
    </div>
    <div id="result-post">
        #result-post content
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
    <div class="results mt-3" style="background-color:green;color:white;">
        DELETE content via .results class
    </div>
    <div id="result-delete">
        #result-delete content
    </div>
    """);
});

htmx.MapPut("/", (HttpRequest request) =>
{
    return Results.Content($"""
    This is the response
    <div id="result-put">
        #result-put content
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
    <div id="result2" class="mt-3" style="background-color:darkblue;color:white;">
        PATCH content via #result2
    </div>
    <div class="results mt-3" style="background-color:lightblue;color:white;">
        PATCH content via .results class
    </div>
    <div id="result-patch">
        #result-patch content
    </div>
    """);
});

app.Run();

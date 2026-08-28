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
                <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
                <style>
                    li{
                        cursor:pointer;
                    }
                </style>
                <meta name="htmx-config" content='{ "antiForgery": {"headerName" : "{{ token.HeaderName}}", "requestToken" : "{{token.RequestToken }}" } }'>
            </head>
            <body>
            <h1>All verbs supported in HTMX</h1>
            <p>Click on the below links to see the response</p>
            <div class="row">
                <div class="col-md-4">
                    <h2>hx-sync="this:queue first"</h2>
                    <p>queue first: Processes only the first click in a rapid succession.</p>
                    <ul hx-sync:inherited="this:queue first" hx-trigger="increment">
                        <li><a hx-get="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#first-get">GET</a> <span id="first-get"></li>
                        <li><a hx-post="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#first-post">POST</a> <span id="first-post"></li>
                        <li><a hx-put="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#first-put">PUT</a> <span id="first-put"></li>
                        <li><a hx-patch="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#first-patch">PATCH</a> <span id="first-patch"></li>
                        <li><a hx-delete="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#first-delete">DELETE</a> <span id="first-delete"></li>
                    </ul>
                </div>
                <div class="col-md-4">
                    <h2>hx-sync="this:queue last"</h2>
                    <p>queue last: Processes the first click and the last click in a rapid succession, dropping intermediate ones.</p>
                    <ul hx-sync:inherited="this:queue last" hx-trigger="increment">
                        <li><a hx-get="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#last-get">GET</a> <span id="last-get"></li>
                        <li><a hx-post="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#last-post">POST</a> <span id="last-post"></li>
                        <li><a hx-put="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#last-put">PUT</a>  <span id="last-put"></li>
                        <li><a hx-patch="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#last-patch">PATCH</a> <span id="last-patch"></li>
                        <li><a hx-delete="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#last-delete">DELETE</a>  <span id="last-delete"></li>
                    </ul>
                </div>
                <div class="col-md-4">
                    <h2>hx-sync="this:queue all"</h2>
                    <p>queue all: Processes every click, but with a delay.</p>
                    <ul hx-sync:inherited="this:queue all" hx-trigger="increment">
                        <li><a hx-get="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#all-get">GET</a> <span id="all-get"></span></li>
                        <li><a hx-post="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#all-post">POST</a> <span id="all-post"></span></li>
                        <li><a hx-put="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#all-put">PUT</a> <span id="all-put"></span></li>
                        <li><a hx-patch="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#all-patch">PATCH</a> <span id="all-patch"></span></li>
                        <li><a hx-delete="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#all-delete">DELETE</a> <span id="all-delete"></span></li>
                    </ul>
                </div>
            </div>

            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js></script>
            <script>
                function incrementCount(target, event) {
                    event.preventDefault();
                    let vals = JSON.parse(target.getAttribute('hx-vals'));
                    vals.count = vals.count + 1;
                    target.textContent = getHtmxVerb(target) + ' ' + vals.count;
                    target.setAttribute('hx-vals', JSON.stringify(vals));
                    htmx.trigger(target, 'increment');
                }

                function getHtmxVerb(element) {
                    // Check for common HTMX verb attributes
                    if (element.hasAttribute('hx-post')) return 'POST';
                    if (element.hasAttribute('hx-get')) return 'GET';
                    if (element.hasAttribute('hx-put')) return 'PUT';
                    if (element.hasAttribute('hx-delete')) return 'DELETE';
                    if (element.hasAttribute('hx-patch')) return 'PATCH';
                    
                    // If no specific verb attribute is found, default to GET
                    return 'GET';
                }

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

htmx.MapGet("/", async (HttpRequest request) =>
{
    await Task.Delay(2000);
    return Results.Content($"GET => {DateTime.UtcNow} =>" + request.Query["count"]);
});

htmx.MapPost("/", async (HttpRequest request) =>
{
    await Task.Delay(2000);
    return Results.Content($"POST => {DateTime.UtcNow} =>" + request.Form["count"]);
});

htmx.MapDelete("/", async (HttpRequest request) =>
{
    await Task.Delay(2000);
    return Results.Content($"DELETE => {DateTime.UtcNow} => " + request.Query["count"]);
});

htmx.MapPut("/", async (HttpRequest request) =>
{
    await Task.Delay(2000);
    return Results.Content($"PUT => {DateTime.UtcNow} => "  + request.Form["count"]);
});

htmx.MapPatch("/", async (HttpRequest request) =>
{
    await Task.Delay(2000);
    return Results.Content($"PATCH => {DateTime.UtcNow} => "  + request.Form["count"]);
});

app.Run();

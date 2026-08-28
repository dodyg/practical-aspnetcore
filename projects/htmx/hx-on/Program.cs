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
            <h1>hx-on</h1>
            <p>This is the complete list of HTMX events you can respond to using hx-on</p>
            <ul>
                <li hx-on:click="alert('click');">hx-on:click</li>
                <li hx-on:mouseover="alert('hover');">hx-on:mouseover</li>
                <li hx-on:dblclick="alert('double click');">hx-on:dblclick</li>
            </ul>
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js></script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

app.Run();



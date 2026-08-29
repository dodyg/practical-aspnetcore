using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

WebApplication.Create();

var builder=  WebApplication.CreateBuilder();
builder.Services.AddAntiforgery();

var app = builder.Build();

app.UseAntiforgery();

app.MapGet("/", (HttpContext context, IAntiforgery antiforgery) =>
{
    var token = antiforgery.GetAndStoreTokens(context);

    var html = $"""
        <!DOCTYPE html>
        <html>
            <head>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">

            </head>
            <body class="container">
                <div class="container">
                    <div class="col-md-6">
                        <h1>Simple Form</h1>
                        <form hx-post="/simple" hx-swap="outerHTML">
                            <input type="hidden" name="{ token.FormFieldName }" value="{token.RequestToken}" />
                            <div class="mb-3">
                                <label for="Name" class="form-label">Name</label>
                                <input type="text" name="Name" class="form-control" />
                            </div>
                            <div class="mb-3">
                                <button type="submit">Post</button>
                            </div>
                        </form>
                    </div>
                </div>
                <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapPost("/simple", (HttpRequest request, [FromForm] Input i) =>
{
    if (request.Headers.ContainsKey("HX-Request") is false)
        return Results.Content("");

    return Results.Content($"""
        <div class="alert alert-success">
            Your data `{i.Name}` has been processed.
        </div>
    """);
});

app.Run();

class Input 
{
    public string Name { get; set; } = string.Empty;
 }


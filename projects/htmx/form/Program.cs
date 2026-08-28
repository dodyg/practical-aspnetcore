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
                <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
            </head>
            <body>
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
                                <button type="submit" class="btn btn-primary">Post</button>
                            </div>
                        </form>
                    </div>
                </div>
                <script src="https://unpkg.com/htmx.org@2.0.0" integrity="sha384-wS5l5IKJBvK6sPTKa2WZ1js3d947pvWXbPJ1OmWfEuxLgeHcEbjUUA5i9V5ZkpCw" crossorigin="anonymous"></script>
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


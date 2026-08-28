var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var html = """
        <html>
            <head>
                <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
                <style>
                    div[hx-trigger] {
                        cursor:pointer;
                    }
                </style>
            </head>
            <body>
                <div class="container">
                    <h1>Modal in Bootstrap 5</h1>
                    <p>A port from this <a href="https://htmx.org/examples/modal-bootstrap/">HTMX example</a></p>

                    <button hx-get="/htmx" hx-target="#designated-modal" hx-trigger="click" data-bs-toggle="modal" data-bs-target="#designated-modal" class="btn btn-primary">Open Modal</button>

                    <div id="designated-modal" class="modal modal-blur fade" style="display: none" aria-hidden="false" tabindex="-1">
                        <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
                            <div class="modal-content"></div>
                        </div>
                    </div>
                    <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js></script>
                    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/htmx", (HttpRequest request, string key) =>
{
    if (request.Headers.ContainsKey("HX-Request") is false)
        return Results.Content("");

    return Results.Content(
        $$"""
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Greetings</h5>
                </div>
                <div class="modal-body">
                    <p>The current UTC time is {{ DateTime.UtcNow }}</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
        """);
});

app.Run();



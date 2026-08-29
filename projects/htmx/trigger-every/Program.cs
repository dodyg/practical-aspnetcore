
var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var html = """
        <!DOCTYPE html>
        <html>
            <head>
                <style>
                    div[hx-get]{
                        cursor:pointer;
                    }
                </style>
            </head>
            <body>
                <div hx-get="/htmx" hx-trigger="every 1s">..wait</div>
                <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/htmx/", (HttpRequest request) =>
{
    if (request.Headers.ContainsKey("HX-Request") is false)
        return Results.Content("");

    return Results.Content($"{DateTime.UtcNow}");
});

app.Run();



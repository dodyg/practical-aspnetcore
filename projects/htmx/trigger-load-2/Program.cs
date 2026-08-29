
var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var html = """
        <html>
            <body>
                <div hx-get="/htmx" hx-trigger="load delay:1s" hx-swap="outerHTML"></div>

                <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/htmx", (HttpRequest request) =>
{
    if (request.Headers.ContainsKey("HX-Request") is false)
        return Results.Content("");

    return Results.Content($"""<div hx-get="/htmx" hx-trigger="load delay:1s" hx-swap="outerHTML">{DateTime.UtcNow}</div>""");
});

app.Run();



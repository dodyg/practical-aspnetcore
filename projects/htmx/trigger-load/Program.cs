
var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var html = """
        <html>
        <head>
            <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        <body class="container">
                <div hx-get="/htmx" hx-trigger="load"></div>

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

    return Results.Content("Hello world from HTMX");
});

app.Run();



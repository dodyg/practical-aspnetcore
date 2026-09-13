var app = WebApplication.Create();
app.MapGet("/", () => Results.Content("""
    <!DOCTYPE html>
    <html>
        <head>
            <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        <body class="container">
            <h1>QUERY request body</h1>
            <input id="term" name="q" value="htmx">
            <button hx-query="/search" hx-include="#term" hx-target="#result">Search</button>
            <pre id="result">The QUERY body appears here.</pre>
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
        </body>
    </html>
    """, "text/html"));

app.MapMethods("/search", ["QUERY"], async (HttpRequest request) =>
{
    using var reader = new StreamReader(request.Body);
    return Results.Content($"QUERY body: {await reader.ReadToEndAsync()}");
});

app.Run();

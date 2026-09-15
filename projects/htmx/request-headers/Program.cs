var app = WebApplication.Create();

app.MapGet("/", () => Results.Content("""
    <!DOCTYPE html>
    <html>
        <head>
            <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        
        <body class="container">
            <h1>Request headers</h1>
            <button id="load" hx-get="/headers" hx-target="#result">Echo htmx headers</button>

            <pre id="result" style="margin-top:15px;"></pre>
            
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
        </body>
        </html>
    """, "text/html"));

app.MapGet("/headers", (HttpRequest request) =>
{
    var names = new[] { "HX-Request", "HX-Source", "HX-Target", "HX-Request-Type", "HX-Trigger-Name", "Accept" };
    return Results.Content(string.Join("\n", names.Select(name => $"{name}: {request.Headers[name]}")));
});

app.Run();

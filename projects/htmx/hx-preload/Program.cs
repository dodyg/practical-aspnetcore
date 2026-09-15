var app = WebApplication.Create();
app.MapGet("/", () => Results.Content("""
    <!DOCTYPE html>
    <html>
        <head>
            <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        <body class="container">
            <h1>Preload on hover</h1>
            <a href="/details" hx-get="/details" hx-target="#result" hx-preload="mouseover">Hover, then click for details</a>
            
            <div id="result"></div>
        
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script><script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/ext/hx-preload.js"></script>

        </body>
    </html>

    """, "text/html"));
app.MapGet("/details", () => Results.Content("<p>Prefetched details are ready.</p>"));


app.Run();

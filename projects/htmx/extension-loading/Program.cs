var app = WebApplication.Create();
app.MapGet("/", () => Results.Content("""
    <!DOCTYPE html>
    <html>
        <head>
            <meta name="htmx-config" content='extensions:"demo"'>
            <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        <body class="container">
            <h1>Direct extension loading</h1><button hx-get="/message" hx-target="#result">Run htmx request</button><p id="result"></p>
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
            <script>htmx.registerExtension('demo', { onEvent(name) { if (name === 'htmx:after:request') console.log('demo extension observed request'); } });</script>
            <p>This page has no <code>hx-ext</code>; the custom extension registers directly.</p>
        </body>
    </html>
    """, "text/html"));

app.MapGet("/message", () => Results.Content("<strong>Extension-enabled response.</strong>"));

app.Run();

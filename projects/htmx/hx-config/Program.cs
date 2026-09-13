var app = WebApplication.Create();
app.MapGet("/", () => Results.Content("""
            <!DOCTYPE html>
            <html>
                <head>
                    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
                </head>
            <body class="container">
                <h1>Per-element hx-config</h1>
                <button hx-get="/data" hx-config='{"timeout":5000,"mode":"same-origin"}' hx-target="#result">JSON config</button>
                <button hx-get="/data" hx-config="timeout:2000" hx-target="#result">HCON config</button>
                <p id="result">Choose a request configuration.</p>
                <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>

            </body>
            </html>

    """, "text/html"));

app.MapGet("/data", () => Results.Content($"Request completed at {DateTime.UtcNow}"));
app.Run();

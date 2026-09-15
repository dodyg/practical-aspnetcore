var app = WebApplication.Create();
app.MapGet("/", () => Results.Content("""
        <!DOCTYPE html>
        <html>
            <head>
                <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
                <style>
                    #loading-indicator {
                        opacity: 0;
                        transition: opacity 150ms ease-in;
                    }

                    #loading-indicator.htmx-request {
                        opacity: 1;
                    }
                </style>
            </head>
            <body class="container">
                <h1>Browser loading indicator</h1>
                <button hx-get="/slow" hx-target="#result" hx-indicator="#loading-indicator" hx-browser-indicator="true">Start request</button>
                <span id="loading-indicator" role="status" aria-live="polite">Loading...</span>
                <p id="result">Watch the tab spinner or the inline indicator.</p>
        
                <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
                <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/ext/hx-browser-indicator.js"></script>
        </body>
        </html>
    """, "text/html"));

app.MapGet("/slow", async () => { await Task.Delay(5000); return Results.Content("Finished."); });
app.Run();

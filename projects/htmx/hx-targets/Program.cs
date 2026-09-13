var app = WebApplication.Create();

app.MapGet("/", () => Results.Content("""
    <!DOCTYPE html>
    <html>
        <head>
            <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        <body class="container">
            <h1>Many targets</h1><button hx-get="/notice" hx-targets=".alert-box">Update all alerts</button>
            <br/>
            <p class="alert-box">First alert</p>
            
            <p class="alert-box">Second alert</p>
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script><script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/ext/hx-targets.js"></script>
        </body>
        </html>
    """, "text/html"));

app.MapGet("/notice", () => Results.Content($"<p class='alert-box'>Updated at {DateTime.UtcNow:HH:mm:ss}</p>"));

app.Run();

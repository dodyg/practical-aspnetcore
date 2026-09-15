var app = WebApplication.Create();
app.MapGet("/", () => Results.Content("""
    <!DOCTYPE html>
    <html>
        <head>
            <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        <body class="container">
            <h1>Consolidated htmx:error</h1>
            <button hx-get="http://127.0.0.1:9/unreachable" hx-target="#result">Network failure</button>
            <button hx-get="/slow" hx-config="timeout:100" hx-target="#result">Timeout</button>
            <p id="result">Errors appear here and in the console.</p>
        
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
            <script>
                document.body.addEventListener('htmx:error', event => 
                { 
                    result.textContent = 'htmx:error: ' + (event.detail.error?.message ?? event.detail.error ?? 'unknown error'); 
                });
            </script>
        </body>
    </html>
    """, "text/html"));

app.MapGet("/slow", async () => { 
    await Task.Delay(1000); 
    return Results.Content("Too late."); 
    });

app.Run();

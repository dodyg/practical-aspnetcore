var app = WebApplication.Create();

app.MapGet("/", () => Results.Content("""
    <!DOCTYPE html>
    <html>
        <head>
            <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        <body class="container">
            <h1>Merge response head content</h1>
            <button hx-get="/details" hx-target="#content">Load details</button>
            
            <main id="content">Nothing loaded.</main>
            
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/ext/hx-head.js"></script>
        </body>
        </html>

    """, "text/html"));

app.MapGet("/details", () => 
    Results.Content("<head hx-head='merge'><title>Details loaded</title><style>.details{color:teal}</style></head><p class='details'>The response head was merged.</p>"));

app.Run();

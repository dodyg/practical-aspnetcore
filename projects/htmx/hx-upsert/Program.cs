var app = WebApplication.Create();
app.MapGet("/", () => Results.Content("""
    <!DOCTYPE html>
    <html>
        <head>
            <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        <body class="container">
            <h1>Update or insert by id</h1>
            <button hx-get="/items" hx-target="#items" hx-swap="upsert">Refresh items</button>

            <ul id="items">
                <li id="item-1">Existing item</li>
                <li id="item-old">Preserved item</li>
            </ul>
            
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script><script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/ext/hx-upsert.js"></script>

        </body>
        </html>

    """, "text/html"));
    
app.MapGet("/items", () => Results.Content("<li id='item-1'>Updated item</li><li id='item-2'>Inserted item</li>"));
app.Run();

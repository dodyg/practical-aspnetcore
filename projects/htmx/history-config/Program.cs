var app = WebApplication.Create();

app.MapGet("/", () => Page("Home", "Welcome", "/one"));
app.MapGet("/one", () => Page("One", "Page one", "/two"));
app.MapGet("/two", () => Page("Two", "Page two", "/"));

app.Run();

static IResult Page(string title, string message, string next)
    => Results.Content($"""
        <!doctype html>
        <html>
            <head>
                <title>{title}</title>
                <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
            </head>
            <body class="container">
                <nav hx-boost="true">
                    <a href="/">Home</a> 
                    <a href="/one">One</a> 
                    <a href="/two">Two</a>
                </nav>
                
                <main hx-history-elt><h1>{message}</h1><a href="{next}">Next page</a></main>

                <p>Back/forward re-fetches this fragment. Try <code>htmx.config.history = \"reload\"</code> or <code>false</code>.</p>
                
                <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
                <script>/* Alternative: htmx.config.history = "reload"; or htmx.config.history = false; */</script>
            </body>
        </html>
        """, "text/html");

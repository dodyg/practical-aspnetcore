var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var html = """
        <html>
            <head>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">

                <style>
                    div[hx-trigger] {
                        cursor:pointer;
                    }
                </style>
            </head>
            <body class="container">
                <div class="container">
                    <h1>Modal with Pico CSS</h1>
                    <p>A native dialog powered by htmx and Pico CSS.</p>

                    <button hx-get="/htmx" hx-target="#designated-modal" hx-trigger="click">Open Modal</button>

                    <dialog id="designated-modal"></dialog>
                    <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
                    <script>
                        document.body.addEventListener("htmx:after:swap", (event) => {
                            if (event.detail.ctx?.target?.id === "designated-modal")
                                document.querySelector("#designated-modal").showModal();
                        });
                    </script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/htmx", (HttpRequest request, string key) =>
{
    if (request.Headers.ContainsKey("HX-Request") is false)
        return Results.Content("");

    return Results.Content(
        $$"""
        <article>
            <header><h2>Greetings</h2></header>
            <p>The current UTC time is {{ DateTime.UtcNow }}</p>
            <footer><button onclick="this.closest('dialog').close()">Close</button></footer>
        </article>
        """);
});

app.Run();



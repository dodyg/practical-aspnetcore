
var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var html = """
        <html>
            <body>
                <div hx-get="/htmx" hx-trigger="load delay:1s" hx-swap="outerHTML"></div>

                <script src="https://unpkg.com/htmx.org@2.0.0" integrity="sha384-wS5l5IKJBvK6sPTKa2WZ1js3d947pvWXbPJ1OmWfEuxLgeHcEbjUUA5i9V5ZkpCw" crossorigin="anonymous"></script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/htmx", (HttpRequest request) =>
{
    if (request.Headers.ContainsKey("HX-Request") is false)
        return Results.Content("");

    return Results.Content($"""<div hx-get="/htmx" hx-trigger="load delay:1s" hx-swap="outerHTML">{DateTime.UtcNow}</div>""");
});

app.Run();



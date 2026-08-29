var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var html = """
        <html>
            <head>
                <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
                <style>
                    div[hx-trigger] {
                        cursor:pointer;
                    }
                </style>
            </head>
            <body>
                <div class="container">
                    <h1>hx-swap-oob for out of band swap</h1>
                    <p>You can find the full documentation <a href="https://htmx.org/attributes/hx-swap/">here</a></p>
                    <div class="row">
                        <div class="col-md-3 m-3">
                            <strong>innerHTML</strong><br/>
                            <div hx-get="/htmx/innerHtml" hx-trigger="click" hx-swap="innerHTML">Click Me</div>
                        </div>
                        <div id="oob-target">This will be replaced once you click above</div>
                    </div>
                </div>
                <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/htmx/{key}", (HttpRequest request, string key) =>
{
    if (request.Headers.ContainsKey("HX-Request") is false)
        return Results.Content("");

    return key switch 
    {
        "innerHtml" => Results.Content($"""
        Hello {DateTime.UtcNow}. <p>Because this is hxSwap="innerHTML", you can keep clicking and the swap keeps working. Check the date. </p>
        <div id="oob-target" hx-swap-oob="true">New out of band message {DateTime.UtcNow}</div>
        """),
        _ => Results.Content("")
    };
});

app.Run();



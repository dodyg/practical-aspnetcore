
var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var html = """
        <!DOCTYPE html>
        <html>
            <head>
                <style>
                    li[hx-get]{
                        cursor:pointer;
                    }
                </style>
            </head>
            <body>
                <h1>Click once</h1>
                <ul>
                    <li hx-get="/htmx/once" hx-trigger="click once">Click</li>
                    <li hx-get="/htmx/unlimited" hx-trigger="click">Click</li>
                </ul>    
                <script src="https://unpkg.com/htmx.org@2.0.0" integrity="sha384-wS5l5IKJBvK6sPTKa2WZ1js3d947pvWXbPJ1OmWfEuxLgeHcEbjUUA5i9V5ZkpCw" crossorigin="anonymous"></script>
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
        "once" => Results.Content($"This is the only result you are going to get {DateTime.UtcNow}"),
        "unlimited" => Results.Content($"You can continue to click {DateTime.UtcNow}"),
        _ => Results.Content("")
    };
});

app.Run();



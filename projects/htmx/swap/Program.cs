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
                    <h1>Various hx-swap options</h1>
                    <p>You can find the full documentation <a href="https://htmx.org/attributes/hx-swap/">here</a></p>
                    <div class="row">
                        <div class="col-md-3 m-3">
                            <strong>innerHTML</strong><br/>
                            <div hx-get="/htmx/innerHtml" hx-trigger="click" hx-swap="innerHTML">Click Me</div>
                        </div>
                        <div class="col-md-3 m-3">
                            <strong>outerHTML</strong><br/>
                            <div hx-get="/htmx/outerHTML" hx-trigger="click" hx-swap="outerHTML">Click Me</div>
                        </div>
                         <div class="col-md-3 m-3">
                            <strong>textContent</strong><br/>
                            <div hx-get="/htmx/textContent" hx-trigger="click" hx-swap="textContent">Click Me</div>
                        </div>
                        <div class="col-md-3 m-3">
                            <strong>beforebegin</strong><br/>
                            <div hx-get="/htmx/beforebegin" hx-trigger="click" hx-swap="beforebegin">Click Me</div>
                        </div>
                        <div class="col-md-3 m-3">
                            <strong>afterbegin</strong><br/>
                            <div hx-get="/htmx/afterbegin" hx-trigger="click" hx-swap="afterbegin">Click Me</div>
                        </div>
                        <div class="col-md-3 m-3">
                            <strong>beforeend</strong><br/>
                            <div hx-get="/htmx/beforeend" hx-trigger="click" hx-swap="beforeend">Click Me</div>
                        </div>
                        <div class="col-md-3 m-3">
                            <strong>afterend</strong><br/>
                            <div hx-get="/htmx/afterend" hx-trigger="click" hx-swap="afterend">Click Me</div>
                        </div>
                        <div class="col-md-3 m-3">
                            <strong>delete</strong><br/>
                            <div hx-get="/htmx/delete" hx-trigger="click" hx-swap="delete">Click Me</div>
                        </div>
                        <div class="col-md-3 m-3">
                            <strong>none</strong><br/>
                            <div hx-get="/htmx/none" hx-trigger="click" hx-swap="none">Click Me</div>
                        </div>
                    </div>
                </div>
                <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js></script>
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
        "innerHtml" => Results.Content($"""Hello {DateTime.UtcNow}. <p>Because this is hxSwap="innerHTML", you can keep clicking and the swap keeps working. Check the date. </p>"""),
        "outerHTML" => Results.Content($"""<div style="background-color:red;color:white">{DateTime.UtcNow}. Click stops working now because you replaced the element itself.</div>"""),
        "textContent" => Results.Content($"""<div style="background-color:blue;color:white">{DateTime.UtcNow}. Everything is treated as text.</div>"""),
        "beforebegin" => Results.Content($"""<div style="background-color:blue;color:white">{DateTime.UtcNow}. Click "Click Me".</div>"""),
        "afterbegin" => Results.Content($"""<div style="background-color:magenta;color:white">{DateTime.UtcNow}. You can click here</div>"""),
        "beforeend" => Results.Content($"""<div style="background-color:brown;color:white">{DateTime.UtcNow}. You can click here.</div>"""),
        "afterend" => Results.Content($"""<div style="background-color:green;color:white">{DateTime.UtcNow}. Click "Click Me"</div>"""),
        _ => Results.Content("")
    };
});

app.Run();



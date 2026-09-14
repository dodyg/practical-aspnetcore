var app = WebApplication.Create();

app.MapGet("/", () => Results.Content("""
    <!DOCTYPE html>
    <html>
        <head>
            <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        <body class="container">
            <h1>Server-Sent HTML events</h1>
            <div hx-sse:connect="/stream" hx-sse:close="done" hx-target="#messages" hx-swap="beforeend"><ul id="messages"></ul></div>
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/ext/hx-sse.min.js"></script>
        </body>
    </html>
    """, "text/html"));

app.MapGet("/stream", async (HttpResponse response, CancellationToken cancellationToken) =>
{
    response.ContentType = "text/event-stream";
    response.Headers.CacheControl = "no-cache";

    for (var i = 1; i <= 3; i++)
    {
        await response.WriteAsync($"data: <li>Event {i}</li>\n\n", cancellationToken);
        await response.Body.FlushAsync(cancellationToken);

        if (i < 3)
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
    }

    await response.WriteAsync("event: done\ndata: Complete\n\n", cancellationToken);
    await response.Body.FlushAsync(cancellationToken);
});

app.Run();

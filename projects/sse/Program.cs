var builder = WebApplication.CreateBuilder();
var app = builder.Build();

int requestCount = 0;

app.MapGet("/sse", async context =>
{
    if (context.Request.Headers["Accept"] == "text/event-stream")
    {
        requestCount++;
        app.Logger.LogDebug($"Start SSE request {requestCount}");

        try
        {
            context.Response.ContentType = "text/event-stream";
            await context.Response.Body.FlushAsync();
            foreach (var round in Counter())
            {
                string data = $"hello world {round}";
                await context.Response.WriteAsync($"data: {data}\n");

                string id = round.ToString();
                await context.Response.WriteAsync($"id: {id}\n");

                string eventType = "message"; //the other type if 'ping'
                await context.Response.WriteAsync($"event: {eventType}\n");

                await context.Response.WriteAsync("\n");//end of message
                await context.Response.Body.FlushAsync();
                await Task.Delay(3000);
            }
        }
        catch (Exception ex)
        {
            app.Logger.LogDebug(ex.Message);
        }
    }
});

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync(@"
    <html>
        <head>
        </head>
        <body>
            <h1>SSE</h1>
            <ul id=""list""></ul>
            <script>
                var source = new EventSource('/sse');
                var list = document.getElementById('list');
                source.onmessage = function(e) {
                    var item = document.createElement('li');
                    item.textContent = e.data;
                    list.appendChild(item);
                };

                source.onerror = function(event){
                    console.log(event);
                };
            </script>
        </body>
    </html>
    ");
});
 
app.Run();

IEnumerable<int> Counter()
{
    int count = 0;
    while (true)
    {
        yield return ++count;
    }
}
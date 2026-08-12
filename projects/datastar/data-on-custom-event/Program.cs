var builder = WebApplication.CreateBuilder();

var app = builder.Build();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync($$"""
    <html>
        <head>
          <script type="module" src="https://cdn.jsdelivr.net/gh/starfederation/datastar@v1.0.2/bundles/datastar.js"></script>
          <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        <body class="container" data-on:my-local-event-3__window="alert(evt.detail.message);">
            <h1>data-on-custom-event</h1>
            <div class="grid" data-on:my-local-event-2="alert(evt.detail.message);" >
                <div><button 
                        data-on:my-local-event="alert(evt.detail.message);" 
                        data-on:click="el.dispatchEvent(new CustomEvent('my-local-event', {  detail : { message : 'hello from my local event' } }))"
                        type="button"
                        >Local Custom Event</button></div>
                <div><button 
                        data-on:click="el.dispatchEvent(new CustomEvent('my-local-event-2', {  bubbles: true, detail : { message : 'hello from my bubbled up event' } }))"
                        type="button"
                        >Bubble Up Local Custom Event</button></div>
                <div><button 
                        data-on:click="window.dispatchEvent(new CustomEvent('my-local-event-3', {  detail : { message : 'hello from my window attached event.' } }))"
                        type="button"
                        >window attached event</button></div>
            </div>
            <br/>
            <h3>All signals on this page</h3>
            <pre data-json-signals></pre>
        </body>
    </html>
    """);
});
 
app.Run();

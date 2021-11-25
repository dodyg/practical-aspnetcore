using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFilter((provider, category, logLevel) =>
{
    return !category.Contains("Microsoft.AspNetCore");
});

var app = builder.Build();

app.UseWebSockets();

app.Use(async (context, next) =>
{
    var log = context.RequestServices.GetService<ILoggerFactory>().CreateLogger("websocket");

    if (!context.WebSockets.IsWebSocketRequest)
    {
        // Not a web socket request
        await next(context);
        return;
    }

    var socket = await context.WebSockets.AcceptWebSocketAsync();
    var bufferSize = new byte[4];
    var receiveBuffer = new ArraySegment<byte>(bufferSize);
    WebSocketReceiveResult result;

    while (socket.State == WebSocketState.Open)
    {
        using (var ms = new MemoryStream())
        {
            int count = 0;
            do
            {
                result = await socket.ReceiveAsync(receiveBuffer, CancellationToken.None);
                if (result.MessageType != WebSocketMessageType.Text)
                    throw new Exception("Unexpected Message");

                ms.Write(receiveBuffer.Array, receiveBuffer.Offset, result.Count);

                receiveBuffer = new ArraySegment<byte>(bufferSize);
                log.LogDebug($"Reading incoming data with buffer(size {bufferSize.Length}) {++count} times");
            }
            while (!result.EndOfMessage && !result.CloseStatus.HasValue);

            ms.Seek(0, SeekOrigin.Begin);

            string clientRequest = string.Empty;
            using (var reader = new StreamReader(ms, Encoding.UTF8))
            {
                clientRequest = await reader.ReadToEndAsync();
            }

            log.LogDebug($"Receive: {clientRequest}");

            var serverReply = Encoding.UTF8.GetBytes("Echo " + clientRequest);
            var replyBuffer = new ArraySegment<byte>(serverReply);
            await socket.SendAsync(replyBuffer, WebSocketMessageType.Text, true, CancellationToken.None);

            if (result.CloseStatus.HasValue)
                break;
        }
    }
});

app.Run(async context =>
{
    context.Response.Headers.Add("content-type", "text/html");
    await context.Response.WriteAsync(@"
<html>                
    <head>
        <script src=""https://code.jquery.com/jquery-3.2.1.min.js"" integrity=""sha256-hwg4gsxgFZhOsEEamdOYGBf13FyQuiTwlAQgxVSNgt4="" crossorigin=""anonymous""></script>
    </head>
    <body>
        <h1>Web Socket</h1>
        <input type=""text"" length=""50"" id=""msg"" value=""hello world""/> <button type=""button"" id=""send"">Send</button>
        <br/>
        <script>
            $(function(){
                var url = ""ws://localhost:5000"";
                var socket = new WebSocket(url);
                var send = $(""#send"");
                var msg = $(""#msg"");

                socket.onopen = function(e){
                    send.click(function(){
                        socket.send(msg.val());                    
                    });
                };

                socket.onmessage = function(e){
                    var response = e.data;
                    alert(response.trim());
                };
            });
        </script>
    </body>
</html>");
});

app.Run();
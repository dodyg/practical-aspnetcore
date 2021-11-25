using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder();

var app = builder.Build();

app.UseWebSockets();
app.Use(async (context, next) =>
{
    if (!context.WebSockets.IsWebSocketRequest)
    {
        // Not a web socket request
        await next(context);
        return;
    }

    var socket = await context.WebSockets.AcceptWebSocketAsync();
    var bufferSize = new byte[1024 * 4];
    var receiveBuffer = new ArraySegment<byte>(bufferSize);
    var result = await socket.ReceiveAsync(receiveBuffer, CancellationToken.None);

    while (!result.CloseStatus.HasValue)
    {
        if (result.MessageType == WebSocketMessageType.Text)
        {
            var clientRequest = Encoding.UTF8.GetString(receiveBuffer.Array, receiveBuffer.Offset, result.Count);

            var serverReply = Encoding.UTF8.GetBytes("Echo " + clientRequest);
            var replyBuffer = new ArraySegment<byte>(serverReply);
            await socket.SendAsync(replyBuffer, WebSocketMessageType.Text, true, CancellationToken.None);

            receiveBuffer = new ArraySegment<byte>(bufferSize);
            result = await socket.ReceiveAsync(receiveBuffer, CancellationToken.None);
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
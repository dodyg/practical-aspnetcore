using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();
app.MapDefaultControllerRoute();
app.UseWebSockets();
app.Run();

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return new ContentResult
        {
            Content = @"
            <html>
            <head>
                <script src=""https://code.jquery.com/jquery-3.2.1.min.js"" integrity=""sha256-hwg4gsxgFZhOsEEamdOYGBf13FyQuiTwlAQgxVSNgt4="" crossorigin=""anonymous""></script>
            </head>
            <body>
                <h1>Web Socket (please open this page at 2 tabs at least)</h1>
                <input type=""text"" length=""50"" id=""msg"" value=""hello world""/>
                <button type=""button"" id=""send"">Send</button>
                <button type=""button"" id=""close"">Close</button>
                <br/>
                <ul id=""responses""></ul>
                <script>
                    $(function(){
                        var url = ""ws://localhost:5000/ws"";
                        var socket = new WebSocket(url);
                        var send = $(""#send"");
                        var close = $(""#close"");
                        var msg = $(""#msg"");
                        var responses = $(""#responses"");

                        socket.onopen = function(e){
                            responses.append(`<li>Socket opened</li>`);
                            send.click(function(){
                                if (socket.readyState !== WebSocket.OPEN){
                                    alert('Socket is closed. Cannot send message.');
                                    return;
                                }
                                socket.send(msg.val());
                            });
                        };

                        close.click(function(){
                            if (socket.readyState !== WebSocket.OPEN){
                                alert('You cannot close this connection because it is already closed');
                                return;
                            }
                            socket.close();
                        });

                        socket.onmessage = function(e){
                            var response = e.data;
                            responses.append(`<li>${e.data.trim()}</li>`);
                        };

                        socket.onclose = function(e){
                            responses.append(`<li>Socket closed</li>`);
                        };
                    });
                </script>
            </body>
            </html>",
            ContentType = "text/html"
        };
    }
    
    [HttpGet("/ws")]
    public async Task<IActionResult> Websocket(CancellationToken token)
    {
        if (!HttpContext.WebSockets.IsWebSocketRequest)
        {
            return NotFound();
        }
        var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();
        var bufferSize = new byte[1024 * 4];
        var receiveBuffer = new ArraySegment<byte>(bufferSize);
        var result = await socket.ReceiveAsync(receiveBuffer, token);

        while (!result.CloseStatus.HasValue && !token.IsCancellationRequested)
        {
            if (result.MessageType == WebSocketMessageType.Text)
            {
                var clientRequest = Encoding.UTF8.GetString(receiveBuffer.Array, receiveBuffer.Offset, result.Count);

                var serverReply = Encoding.UTF8.GetBytes("Echo " + clientRequest);
                var replyBuffer = new ArraySegment<byte>(serverReply);
                await socket.SendAsync(replyBuffer, WebSocketMessageType.Text, true, token);

                receiveBuffer = new ArraySegment<byte>(bufferSize);
                result = await socket.ReceiveAsync(receiveBuffer, token);
            }
        }
        return Ok();
    }
}
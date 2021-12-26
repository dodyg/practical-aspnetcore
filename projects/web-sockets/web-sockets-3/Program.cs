using System.Net.WebSockets;
using System.Text;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<ConnectionManager>();

async Task ReceiveAsync(ILogger log, WebSocket socket, string socketId, Func<string, Task> responseHandlerAsync)
{
    var bufferSize = new byte[4]; //This is especially small just to exercise the code that handles data that is larger than buffer
    var receiveBuffer = new ArraySegment<byte>(bufferSize);
    WebSocketReceiveResult result;

    while (socket.State == WebSocketState.Open)
    {
        using (var ms = new MemoryStream())
        {
            do
            {
                result = await socket.ReceiveAsync(receiveBuffer, CancellationToken.None);
                if (result.MessageType != WebSocketMessageType.Text)
                    throw new Exception("Unexpected Message");

                ms.Write(receiveBuffer.Array, receiveBuffer.Offset, result.Count);
            }

            while (!result.EndOfMessage && !result.CloseStatus.HasValue);

            ms.Seek(0, SeekOrigin.Begin);

            string clientRequest = string.Empty;
            using (var reader = new StreamReader(ms, Encoding.UTF8))
            {
                clientRequest = reader.ReadToEnd();
            }

            log.LogDebug($"Socket Id {socketId} : Receive: {clientRequest}");

            await responseHandlerAsync(clientRequest);

            if (result.CloseStatus.HasValue)
                break;
        }
    }
}

var app = builder.Build();
app.UseWebSockets();

int count = 0;
app.Use(async (context, next) =>
{
    var log = context.RequestServices.GetService<ILoggerFactory>().CreateLogger("app");
    var cm = context.RequestServices.GetService<ConnectionManager>();

    if (!context.WebSockets.IsWebSocketRequest)
    {
        await next(context);
        return;
    }

    var socket = await context.WebSockets.AcceptWebSocketAsync();
    var socketId = cm.AddSocket(socket);

    await ReceiveAsync(log, socket, socketId, async (clientRequest) =>
    {
        var serverReply = Encoding.UTF8.GetBytes($"Echo {++count} {clientRequest}");
        var replyBuffer = new ArraySegment<byte>(serverReply);
        await socket.SendAsync(replyBuffer, WebSocketMessageType.Text, true, CancellationToken.None);

        var broadcastReply = Encoding.UTF8.GetBytes($"Broadcast {count} {clientRequest}");
        var broadcastBuffer = new ArraySegment<byte>(broadcastReply);

        var socketTasks = new List<Task>();

        foreach (var (s, sid) in cm.Other(socketId))
        {
            socketTasks.Add(s.SendAsync(broadcastBuffer, WebSocketMessageType.Text, true, CancellationToken.None));
            log.LogDebug($"Broadcasting to : {sid}");
        }

        await Task.WhenAll(socketTasks);
    });
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
        <h1>Web Socket (please open this page at 2 tabs at least)</h1>
        <input type=""text"" length=""50"" id=""msg"" value=""hello world""/> <button type=""button"" id=""send"">Send</button>
        <br/>
        <ul id=""responses""></ul>
        <script>
            $(function(){
                var url = ""ws://localhost:5000"";
                var socket = new WebSocket(url);
                var send = $(""#send"");
                var msg = $(""#msg"");
                var responses = $(""#responses"");

                socket.onopen = function(e){
                    send.click(function(){
                        socket.send(msg.val());                    
                    });
                };

                socket.onmessage = function(e){
                    var response = e.data;
                    responses.append(`<li>${e.data.trim()}</li>`);
                };
            });
        </script>
    </body>
</html>");
});

app.Run();


public class ConnectionManager
{
    ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

    public string AddSocket(WebSocket socket)
    {
        var id = Guid.NewGuid().ToString();

        if (!_sockets.TryAdd(id, socket))
            throw new Exception($"Problem in adding socket with Id {id}");

        return id;
    }

    public List<(WebSocket socket, string id)> Other(string id) => _sockets.Where(x => x.Key != id).Select(x => (socket: x.Value, id: x.Key)).ToList();
}
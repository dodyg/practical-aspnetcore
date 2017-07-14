using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Net.WebSockets;
using System;
using System.Threading;
using System.Text;
using System.IO;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StartupBasic
{
    public class ConnectionManager
    {
        ConcurrentDictionary<string, (WebSocket socket, string nickname)> _sockets = new ConcurrentDictionary<string, (WebSocket socket, string nickanme)>();

        public string AddSocket(WebSocket socket)
        {
            var id = Guid.NewGuid().ToString();

            if (!_sockets.TryAdd(id, (socket, string.Empty)))
                throw new Exception($"Problem in adding socket with Id {id}");

            return id;
        }

        public bool RemoveSocket(string id) => _sockets.TryRemove(id, out (WebSocket, string) _);

        public List<(WebSocket socket, string id, string nickname)> Other(string id) => 
            _sockets.Where(x => x.Key != id)
            .Select(x => (x.Value.socket, x.Key, x.Value.nickname))
            .ToList();
    }

    public enum CommandType {
        List,
        Send,

        Quit
    }

    public class Command
    {
        public CommandType Type {get; set;}

        public (string, string, string) Data { get; set;}
    }

    public class CommandHandler
    {
        public (bool, Command) Parse(string cmd)
        {
            try
            {
                if (cmd.StartsWith("#"))
                {
                    var segment = cmd.Split(new [] { ' '});

                    if (segment.Length > 0)
                    {
                        switch(segment[0])
                        {
                            case "#list" : return (true, new Command { Type = CommandType.List,  Data = ("", "","") });
                            case "#quit" : return (true, new Command { Type = CommandType.Quit, Data = ("", "", "")});
                            default : return (false, null);
                        }
                    }
                }

                return (false, null);
            }   
            catch
            {
                return (false, null);
            } 
        }
    }

    public class Startup
    {
        async Task ReceiveAsync(ConnectionManager cm, ILogger log, WebSocket socket, string socketId, Func<ConnectionManager, string, Task> responseHandlerAsync)
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
                        
                        if (result.MessageType == WebSocketMessageType.Close){
                            log.LogDebug($"Socket Id {socketId} : Receive closing message.");
                            var removalStatus = cm.RemoveSocket(socketId);
                            log.LogDebug($"Socket Id {socketId} removal status {removalStatus}.");
                            break;
                        }

                        if (result.MessageType != WebSocketMessageType.Text)
                            throw new Exception("Unexpected Message");

                        ms.Write(receiveBuffer.Array, receiveBuffer.Offset, result.Count);
                    }
                    while (!result.EndOfMessage && !result.CloseStatus.HasValue);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        ms.Seek(0, SeekOrigin.Begin);

                        string clientRequest = string.Empty;
                        using (var reader = new StreamReader(ms, Encoding.UTF8))
                        {
                            clientRequest = reader.ReadToEnd();
                        }

                        log.LogDebug($"Socket Id {socketId} : Receive: {clientRequest}");

                        await responseHandlerAsync(cm, clientRequest);
                    }
                    
                    if (result.CloseStatus.HasValue)
                        break;
                }
            }
        }

        public ArraySegment<byte> Reply(string content ) => new ArraySegment<byte>(Encoding.UTF8.GetBytes(content));

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            logger.AddConsole((str, level) => !str.Contains("Microsoft.AspNetCore") && level >= LogLevel.Trace);

            var log = logger.CreateLogger("");

            app.UseWebSockets();

            var cm = new ConnectionManager();

            int count = 0;
            app.Use(async (context, next) =>
            {
                if (!context.WebSockets.IsWebSocketRequest)
                {
                    await next();
                    return;
                }

                var socket = await context.WebSockets.AcceptWebSocketAsync();
                var socketId = cm.AddSocket(socket);

                var cmdHandler = new CommandHandler();
                await ReceiveAsync(cm, log, socket, socketId, async (connectionManager, clientRequest) =>
                {
                    var (isOK, cmd) = cmdHandler.Parse(clientRequest);

                    if (isOK)
                    {
                        switch(cmd.Type)
                        {
                            case CommandType.List : 
                            {
                                var others = connectionManager.Other(socketId).Select(x => x.nickname).ToList();
                                if (others.Count > 0)
                                    await socket.SendAsync(Reply(string.Join(",", others)), WebSocketMessageType.Text, true, CancellationToken.None);
                                else
                                    await socket.SendAsync(Reply("No other user on this channel"), WebSocketMessageType.Text, true, CancellationToken.None);
                                break;
                            }

                            case CommandType.Quit :
                            {
                                connectionManager.RemoveSocket(socketId);
                                await socket.SendAsync(Reply("Quitting chat"), WebSocketMessageType.Text, true, CancellationToken.None);
                                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                                break;
                            }

                            default :
                            {
                                await socket.SendAsync(Reply("Command not understood"), WebSocketMessageType.Text, true, CancellationToken.None);
                                break;
                            }
                        }
                        var serverReply = Encoding.UTF8.GetBytes($"Echo {++count} {clientRequest}");
                        var replyBuffer = new ArraySegment<byte>(serverReply);
                        await socket.SendAsync(replyBuffer, WebSocketMessageType.Text, true, CancellationToken.None);

                        var broadcastReply = Encoding.UTF8.GetBytes($"Broadcast {count} {clientRequest}");
                        var broadcastBuffer = new ArraySegment<byte>(broadcastReply);

                        var socketTasks = new List<Task>();

                        foreach (var (s, sid, nickname) in connectionManager.Other(socketId))
                        {
                            socketTasks.Add(s.SendAsync(broadcastBuffer, WebSocketMessageType.Text, true, CancellationToken.None));
                            log.LogDebug($"Broadcasting to : {sid}");
                        }

                        await Task.WhenAll(socketTasks);
                    }
                    else
                    {
                        await socket.SendAsync(Reply("Command not understood"), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                });

                if (socket.State != WebSocketState.Open)
                {
                    log.LogDebug($"Socket Id {socketId} with status {socket.State}");
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
        <h1>Web Socket (please open this page at 2 tabs at least)</h1>
        <input type=""text"" length=""50"" id=""msg"" value=""hello world""/> 
        <button type=""button"" id=""send"">Send</button>
        <button type=""button"" id=""close"">Close</button>
        <br/>
        <ul id=""responses""></ul>
        <script>
            $(function(){
                var url = ""ws://localhost:5000"";
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
</html>");
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
              .UseKestrel()
              .UseStartup<Startup>()
              .Build();

            host.Run();
        }
    }
}
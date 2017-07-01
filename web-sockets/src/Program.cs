using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Net.WebSockets;
using System;
using System.Threading;
using System.Text;

namespace StartupBasic 
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are two services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            app.UseWebSockets();
            
            app.Use(async (context, next) =>
            {
                if (!context.WebSockets.IsWebSocketRequest)
                {
                    // Not a web socket request
                    await next();
                    return;
                }

                var socket = await context.WebSockets.AcceptWebSocketAsync();
                while(socket.State == WebSocketState.Open)
                {
                    var buffer = new ArraySegment<byte>(new byte[1024 * 4]);
                    var result = await socket.ReceiveAsync(buffer, CancellationToken.None);    

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var clientMessage = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count); 

                        var serverReply = Encoding.UTF8.GetBytes("Echo " + clientMessage);
                        buffer = new ArraySegment<byte>(serverReply);

                        await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
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
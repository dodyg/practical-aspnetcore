using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore;

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

        IEnumerable<int> Counter()
        {
            int count = 0;
            while(true)
            {
                yield return ++count;
            }
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            logger.AddConsole((str, level) =>
            {
                return !str.Contains("Microsoft.AspNetCore") && level >= LogLevel.Trace;
            });

            var log = logger.CreateLogger("Startup");
            int requestCount = 0;

            app.Use(async (context, next) =>
            {
                if (context.Request.Headers["Accept"] == "text/event-stream")
                {
                    requestCount++;
                    log.LogDebug($"Start SSE request {requestCount}");

                    context.Response.ContentType = "text/event-stream";
                    context.Response.Body.Flush();

                    foreach (var round in Counter())
                    {
                        string data = $"hello world {round}";
                        await context.Response.WriteAsync($"data: {data}\n");

                        string id = round.ToString();
                        await context.Response.WriteAsync($"id: {id}\n");

                        string eventType = "message"; //the other type if 'ping'
                        await context.Response.WriteAsync($"event: {eventType}\n");

                        await context.Response.WriteAsync("\n");//end of message
                        context.Response.Body.Flush();
                        await Task.Delay(3000);
                    }

                    context.RequestAborted.WaitHandle.WaitOne();
                    return;
                }

                await next();
            });

            app.Run(context =>
            {
                return context.Response.WriteAsync(@"
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
                                alert('error');
                            };
                        </script>
                    </body>
                </html>
                ");
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System;

namespace PracticalAspNetCore
{
    public class Startup
    {
        IEnumerable<int> Counter()
        {
            int count = 0;
            while (true)
            {
                yield return ++count;
            }
        }
        public void Configure(IApplicationBuilder app, ILogger<Startup> log)
        {
            int requestCount = 0;

            app.UseRouting();
            app.UseEndpoints(route =>
            {
                route.MapGet("/sse", async context =>
                {
                    if (context.Request.Headers["Accept"] == "text/event-stream")
                    {
                        requestCount++;
                        log.LogDebug($"Start SSE request {requestCount}");

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
                            log.LogDebug(ex.Message);
                        }
                    }
                });
            });

            app.Run(async context =>
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
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                ).ConfigureLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddConsole();
                    builder.AddFilter((provider, category, logLevel) =>
                    {
                        return !category.Contains("Microsoft.AspNetCore");
                    });
                });
    }
}
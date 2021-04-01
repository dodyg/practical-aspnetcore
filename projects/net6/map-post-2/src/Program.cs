using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace PracticalAspNetCore
{
    public record Greeting(string Message);

    public static class MyApi
    {
        public static JsonResult Greet([FromBody] Greeting greet) => new JsonResult(new { greet.Message });
    }

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
                
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapPost("/greet",(Func<Greeting, JsonResult>) MyApi.Greet);
           
                endpoints.MapGet("/", async context =>
                {
                    string page = @"<!doctype html><html><body>
                            <div id=""response""></div><button type=""button"" onclick=""send()"">Click</button>
                            <script>
                                async function postData(url = '', data = {}) {
                                const response = await fetch(url, {
                                    method: 'POST', 
                                    headers: {
                                    'Content-Type': 'application/json'
                                    },
                                    body: JSON.stringify({ message : ""Hello World""}) 
                                });
                                return response.json(); // parses JSON response into native JavaScript objects
                                }
                                
                                function send(){
                                    postData(window.location + 'greet', { answer: 42 })
                                    .then(data => {
                                        document.getElementById(""response"").innerHTML = data.message;
                                    });
                                }
                            </script>
                        </body></html>
                    ";

                    context.Response.Headers.Append("Content-Type", "text/html");
                    await context.Response.WriteAsync(page);
                });
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}
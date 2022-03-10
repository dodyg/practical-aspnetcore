using Microsoft.AspNetCore.Mvc;

object Greet([FromBody] Greeting greet, HttpContext context) 
            => new { Message = context.Request.Method + ":" + greet.Message };

var app = WebApplication.Create();

app.MapMethods("/greet", new[] { "POST", "PUT", "PATCH" }, Greet);

app.MapGet("/", async context =>
{
    string page = @"<!doctype html><html><body>
            <div id=""response""></div>
            <button type=""button"" onclick=""send('POST')"">Send POST</button>
            <button type=""button"" onclick=""send('PUT')"">Send PUT</button>
            <button type=""button"" onclick=""send('PATCH')"">Send PATCH</button>
            <script>
                async function postData(method, url = '', data = {}) {
                const response = await fetch(url, {
                    method: method, 
                    headers: {
                    'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ message : ""Hello World""}) 
                });
                return response.json(); // parses JSON response into native JavaScript objects
                }
                
                function send(method){
                    postData(method, window.location + 'greet', { answer: 42 })
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

app.Run();

public record Greeting(string Message);


using Microsoft.AspNetCore.Mvc;

IResult Greet([FromBody] Greeting greet) => Results.Json(new { greet.Message });

var app = WebApplication.Create();
app.MapPost("/greet", Greet);

app.MapGet("/", async context =>
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

app.Run();

public record Greeting(string Message);

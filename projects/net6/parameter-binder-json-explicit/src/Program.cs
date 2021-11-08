using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

var web = WebApplication.Create();

web.MapPost("/greet", ([FromBody] Greeting greet) => Results.Json(new {  Message = greet.Message + " from the server" }));

web.MapGet("/", () => Results.Text(@"<!doctype html><html><body>
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
                    ", "text/html"));

web.Run();

public record Greeting(string Message);

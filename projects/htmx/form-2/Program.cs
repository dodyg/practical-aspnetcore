using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

WebApplication.Create();

var builder=  WebApplication.CreateBuilder();
builder.Services.AddAntiforgery();

var app = builder.Build();

app.UseAntiforgery();

app.MapGet("/", (HttpContext context, IAntiforgery antiforgery) =>
{
    var token = antiforgery.GetAndStoreTokens(context);

    var html = $"""
        <!DOCTYPE html>
        <html>
            <head>
                <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
            </head>
            <body>
                <main class="container">
                    <h1>Simple Form</h1>
                    <form hx-post="/simple" hx-swap="outerHTML">
                        <input type="hidden" name="{ token.FormFieldName }" value="{token.RequestToken}" />

                        <label for="name">Name</label>
                        <input type="text" name="Name" id="name" />

                        <label for="bio">Bio</label>
                        <textarea name="Bio" id="bio" rows="5"></textarea>

                        <label for="gender">Gender</label>
                        <select name="Gender" id="gender">
                            <option>Non Binary</option>
                            <option>Male</option>
                            <option>Female</option>
                        </select>

                        <label>
                            <input type="hidden" name="IsEmployed" value="false" />
                            <input name="IsEmployed" type="checkbox" value="true" />
                            Is Employed
                        </label>

                        <fieldset>
                            <legend>Preferred Transportation</legend>
                            <label>
                                <input type="radio" name="Transportation" value="car" />
                                Car
                            </label>
                            <label>
                                <input type="radio" name="Transportation" value="metro/subway" checked />
                                Metro/Subway
                            </label>
                        </fieldset>

                        <button type="submit">Post</button>
                    </form>
                </main>
                <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapPost("/simple", (HttpRequest request, [FromForm] Input i) =>
{
    if (request.Headers.ContainsKey("HX-Request") is false)
        return Results.Content("");

    return Results.Content($"""
        <div class="alert alert-success mb-3">
            Your data has been processed.
        </div>
        
        <table class="table">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Value</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Name</td>
                    <td>{i.Name}</td>
                </tr>
                <tr>
                    <td>Bio</td>
                    <td>{i.Bio}</td>
                </tr>
                <tr>
                    <td>Gender</td>
                    <td>{i.Gender}</td>
                </tr>
                <tr>
                    <td>Is Employed</td>
                    <td>{i.IsEmployed}</td>
                </tr>
                <tr>
                    <td>Transportation</td>
                    <td>{i.Transportation}</td>
                </tr>
            </tbody>
        </table>
    """);
});

app.Run();

class Input 
{
    public string Name { get; set; } = string.Empty;

    public string Bio { get; set;} = string.Empty;

    public string Gender { get; set;} = string.Empty;

    public bool IsEmployed { get; set; }

    public string Transportation { get; set; } = string.Empty;    
 }


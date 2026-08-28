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
                <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
            </head>
            <body>
                <div class="container">
                    <div class="col-md-6">
                        <h1>Simple Form</h1>
                        <form hx-post="/simple" hx-swap="outerHTML">
                            <input type="hidden" name="{ token.FormFieldName }" value="{token.RequestToken}" />
                            <div class="mb-3">
                                <label for="name" class="form-label">Name</label>
                                <input type="text" name="Name" id="name" class="form-control" />
                            </div>
                            <div class="mb-3">
                                <label for="bio" class="form-label">Bio</label>
                                <textarea name="Bio" id="bio" rows="5" class="form-control">
                                </textarea>
                            </div>
                            <div class="mb-3">
                                <label for="Gender" id="gender" class="form-label">Gender</label>
                                <select name="Gender" id="gender" class="form-select">
                                    <option>Non Binary</option>
                                    <option>Male</option>
                                    <option>Female</option>
                                </select>
                            </div>
                            <div class="form-check mb-3">
                                <input type="hidden" name="IsEmployed" value="false">
                                <input class="form-check-input" name="IsEmployed" type="checkbox" value="true" id="isEmployed">
                                <label class="form-check-label" for="isEmployed">
                                    Is Employed
                                </label>
                            </div>
                            <div>
                                Preferred Transportation
                            </div>
                            <div class="form-check">
                                <input class="form-check-input" type="radio" name="Transportation" id="transportation1" value="car">
                                <label class="form-check-label" for="transportation1">
                                    Car
                                </label>
                            </div>
                            <div class="form-check mb-3">
                                <input class="form-check-input" type="radio" name="Transportation" id="transportation2" value="metro/subway" checked>
                                <label class="form-check-label" for="transportation2">
                                    Metro/Subway
                                </label>
                            </div>
                            <div class="mb-3">
                                <button type="submit" class="btn btn-primary">Post</button>
                            </div>
                        </form>
                    </div>
                </div>
                <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js></script>
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


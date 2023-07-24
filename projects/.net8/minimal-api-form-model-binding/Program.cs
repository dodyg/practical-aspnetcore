using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddAntiforgery();
var app = builder.Build();

app.MapGet("/", (HttpContext context, IAntiforgery antiforgery) =>
{
    var token = antiforgery.GetAndStoreTokens(context);
    return Results.Content(Template($$"""
            <div class="row">
                <div class="col-md-6">
                    <form method="POST" action="/">
                        <input name="{{token.FormFieldName}}" type="hidden" value="{{token.RequestToken}}" />
                        <div class="mb-3">
                            <label for="Title" class="form-label">Title</label>
                            <input type="text" class="form-control" id="Title" name="Title">
                        </div>
                        <div class="mb-3">
                            <label for="Body" class="form-label">Body</label>
                            <textarea class="form-control" id="Body" name="Body" rows="3"></textarea>
                        </div>
                        <button type="submit" class="btn btn-primary">Submit</button>
                    </form>
                </div>
            </div>
    """), "text/html");
});

app.MapPost("/", async ([FromForm] BlogPostInput input, HttpContext context, IAntiforgery antiforgery) =>
{
    try
    {
        await antiforgery.ValidateRequestAsync(context);
        return Results.Content(Template($$"""
        <div class="row">
            <div class="col-md-6">
                Title : {{input.Title}}<br/>
                Body : {{input.Body}}
            </div>
        </div>
"""), "text/html");
    }
    catch (AntiforgeryValidationException)
    {
        return TypedResults.BadRequest("Invalid anti-forgery token");
    }
});


app.Run();

static string Template(string body)
{
    return $$"""
    <html>
    <head>
      <title>Form Model Binding</title>
      <link href="https://fastly.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous">
    </head>
    <body>
        <div class="container">
            <h1>Form Model Binding</h1>
            {{body}}
        </div>
    </body>
    </html>
    """;
}

public class BlogPostInput(string? title, string body)
{
    public string? Title { get; set; } = title;
    public string Body { get; set; } = body;

    public BlogPostInput() : this(null, string.Empty)
    {

    }
}

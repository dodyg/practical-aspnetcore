using Scalar.AspNetCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder();

builder.Services.AddOpenApi(options =>
{
    options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_2;
});

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapGet("/", () =>
{
    var html = """
    <html>
    <body>
        <h1>OpenAPI support for HTTP Query</h1>
        <ul>
            <li>
                You can check the generated OpenAPI document <a href="/openapi/v1.json">here</a>.
            </li>
            <li>
                You can check the Scalar UI <a href="/scalar">here</a>.
            </li>
    </body>
    </html>
    """;

    return TypedResults.Content(html, "text/html");
}).ExcludeFromDescription(); //This is not an API endpoint


app.MapMethods("/search", ["QUERY"], Hello).WithName("Search");
    
app.Run();


static partial class Program
{
    public static string Hello(Name name)
    {
        return $"Hello, {name}";
    }
}

 public record Name(string FirstName, string LastName);

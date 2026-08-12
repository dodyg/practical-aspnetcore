using Microsoft.AspNetCore.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.MapOpenApi("/openapi/{documentName}.yaml");

app.MapGet("/", () =>
{
    var html = """
    <html>
    <body>
        <h1>OpenAPI YAML Endpoint</h1>
        <p>OpenAPI can be served in both JSON and YAML formats in .NET 10.</p>
        <ul>
            <li><a href="/openapi/v1.json">OpenAPI JSON</a></li>
            <li><a href="/openapi/v1.yaml">OpenAPI YAML</a></li>
            <li><a href="/scalar">Scalar UI</a></li>
        </ul>
    </body>
    </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/hello/{name}", (string name) => $"Hello, {name}");

app.Run();

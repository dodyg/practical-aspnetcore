using System.Net.Mime;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
var app = builder.Build();

app.MapOpenApi();

app.MapGet("/", () => Results.Text("""
    <html><body>
        <h1>Binary file responses in OpenAPI</h1>
        <ul>
            <li><a href="/openapi/v1.json">OpenAPI document</a></li>
            <li><a href="/file">GET /file</a> download a PDF-ish blob</li>
        </ul>
    </body></html>
    """, "text/html")).ExcludeFromDescription();

app.MapGet("/file", () => TypedResults.File("PDF-1.4 sample binary payload"u8.ToArray()))
   .Produces<FileContentHttpResult>(contentType: MediaTypeNames.Application.Octet);

app.Run();

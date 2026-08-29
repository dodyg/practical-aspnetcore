using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(options =>
{
    // Union bodies/responses are described with anyOf; the open-api-12 sample
    // shows why we pin the document version to 3.2.
    options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_2;
});

var app = builder.Build();

app.MapOpenApi();

app.MapGet("/", () => Results.Text("""
    <html><body>
        <h1>C# union types in minimal APIs</h1>
        <ul>
            <li><a href="/pets/0">GET /pets/0</a> -> a Dog</li>
            <li><a href="/pets/1">GET /pets/1</a> -> a Cat</li>
            <li>POST /pets with a JSON body like {"name":"Rex"} or {"lives":9}</li>
            <li><a href="/openapi/v1.json">OpenAPI document</a> (anyOf schemas)</li>
        </ul>
    </body></html>
    """, "text/html")).ExcludeFromDescription();

// Union as a return type.
app.MapGet("/pets/{id}", Pet (int id) => id == 0 ? new Dog("Rex") : new Cat(9));

// Union as a request body. The [JsonUnion] classifier (registered below)
// disambiguates the cases from the JSON shape.
app.MapPost("/pets", (Pet pet) => Results.Ok(pet));

app.Run();

public record class Dog(string Name);
public record class Cat(int Lives);

// Without a classifier, deserialization fails when several cases can match the
// same JSON value type ("object" in this case). The classifier inspects the
// JSON and picks the case type.
[JsonUnion(TypeClassifier = typeof(PetClassifierFactory))]
public union Pet(Dog, Cat);

public sealed class PetClassifierFactory : JsonTypeClassifierFactory
{
    public override bool CanClassify(JsonTypeClassifierContext context) =>
        context.Kind == JsonTypeClassifierKind.Union && context.DeclaringType == typeof(Pet);

    public override JsonTypeClassifier CreateJsonClassifier(
        JsonTypeClassifierContext context, JsonSerializerOptions options)
    {
        return (ref Utf8JsonReader reader) =>
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            if (doc.RootElement.TryGetProperty("name", out _)) return typeof(Dog);
            if (doc.RootElement.TryGetProperty("lives", out _)) return typeof(Cat);
            throw new JsonException("Cannot classify Pet from JSON shape.");
        };
    }
}

using System.Text.Json;
using System.Text.Json.Serialization;
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
    <html>
    <head>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
    </head>
    <body class="container">
        <h1>C# union types in minimal APIs</h1>
        <ul>
            <li><a href="/pets/0">GET /pets/0</a> -> a Dog</li>
            <li><a href="/pets/1">GET /pets/1</a> -> a Cat</li>
            <li><a href="/openapi/v1.json">OpenAPI document</a> (anyOf schemas)</li>
        </ul>

        <h2>POST /pets</h2>
        <form data-pet-form="dog">
            <label>
                Dog name
                <input name="name" value="Rex" required />
            </label>
            <button type="submit">Create dog</button>
        </form>

        <form data-pet-form="cat">
            <label>
                Cat lives
                <input name="lives" type="number" value="9" min="1" required />
            </label>
            <button type="submit">Create cat</button>
        </form>

        <pre id="result" aria-live="polite"></pre>

        <script>
            for (const form of document.querySelectorAll("[data-pet-form]")) {
                form.addEventListener("submit", async (event) => {
                    event.preventDefault();

                    const formData = new FormData(form);
                    const pet = form.dataset.petForm === "dog"
                        ? { name: formData.get("name") }
                        : { lives: Number(formData.get("lives")) };
                    const response = await fetch("/pets", {
                        method: "POST",
                        headers: { "Content-Type": "application/json" },
                        body: JSON.stringify(pet)
                    });

                    document.getElementById("result").textContent =
                        `${response.status}: ${await response.text()}`;
                });
            }
        </script>
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

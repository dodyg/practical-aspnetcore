using System.Text.Json.Serialization.Metadata;
using System.Text.Json;

var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    TypeInfoResolver = new DefaultJsonTypeInfoResolver
    {
        Modifiers = { NumberAsString }
    }
};

static void NumberAsString (JsonTypeInfo typeInfo)
{
    if (typeInfo.Type != typeof(People))
        return;

    foreach(JsonPropertyInfo propInfo in typeInfo.Properties)
    {
        if (propInfo.PropertyType == typeof(int) && propInfo.Name == "age")
            propInfo.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.WriteAsString;
    }
}

var app = WebApplication.Create();

app.MapGet("/", () =>
{
    var people = new List<People> 
    {
        new People("Anne", 37, 165),
        new People("John", 23, 180),
        new People("Megan", 34, 150)
    };

    var list = JsonSerializer.Serialize(people, options);

    return Results.Text(list, "application/json");
});

app.Run();

public record People(string Name, int Age, int Height);


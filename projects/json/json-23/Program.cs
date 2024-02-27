using System.Text.Json.Serialization.Metadata;
using System.Text.Json;

var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    TypeInfoResolver = new DefaultJsonTypeInfoResolver
    {
        Modifiers = { IgnoreNegativeValues }
    }
};

static void IgnoreNegativeValues (JsonTypeInfo typeInfo)
{
    if (typeInfo.Type != typeof(People))
        return;

    foreach(JsonPropertyInfo propInfo in typeInfo.Properties)
    {
        if (propInfo.PropertyType == typeof(int))
            propInfo.ShouldSerialize = static (obj, val) => (int)val > 0;
    }
}

var app = WebApplication.Create();

app.MapGet("/", () =>
{
    var people = new List<People> 
    {
        new People("Anne", 37),
        new People("John", 23),
        new People("Megan", -12)
    };

    var list = JsonSerializer.Serialize(people, options);

    return Results.Text(list, "application/json");
});

app.Run();

public record People(string Name, int Age);


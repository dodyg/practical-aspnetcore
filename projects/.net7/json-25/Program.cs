using System.Text.Json.Serialization.Metadata;
using System.Text.Json;

var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    TypeInfoResolver = new DefaultJsonTypeInfoResolver
    {
        Modifiers = { AddTimeStamp }
    }
};

static void AddTimeStamp (JsonTypeInfo typeInfo)
{
    if (typeInfo.Kind != JsonTypeInfoKind.Object && 
        typeInfo.Properties.All(prop => prop.Name == "timestamp"))
        return;

    var timestamp = typeInfo.CreateJsonPropertyInfo(typeof(DateTime), "timestamp");
    timestamp.Get = x => DateTime.UtcNow;
    typeInfo.Properties.Add(timestamp);
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


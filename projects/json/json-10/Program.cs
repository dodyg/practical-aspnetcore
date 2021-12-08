using System.Text.Json;
using System.Text.Json.Serialization;

var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var payload = new List<Person> {
        new Person
        {
            Name = "Annie",
            Age = 33,
            IsMarried = null,
            CurrentTime = DateTimeOffset.UtcNow,
            IsWorking = true
        },
        new Person
        {
            Name = "Dody",
            Age = 42,
            IsMarried = false,
            IsHealthy = true
        },
    };

    var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, //Pay attention here with interaction with the IsMarried and IsWorking properties
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
    };

    return Results.Json(payload, options);
});

app.Run();

public class Person
{
    public string Name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public int? Age { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool? IsMarried { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTimeOffset CurrentTime { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]// Do not serialize this property when null
    public bool? IsWorking { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool IsHealthy { get; set; }
}


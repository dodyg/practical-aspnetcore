using System.Text.Json;
using System.Text.Json.Serialization;

var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var payload = new Person
    {
        Name = "Annie",
        Age = 33,
        IsMarried = false,
        CurrentTime = DateTimeOffset.UtcNow,
        Characters = new Dictionary<string, bool>
        {
            {"Funny" , true},
            {"Feisty" , true},
            {"Brilliant" , true},
            {"FOMA", false}
        },
        IsWorking = true
    };

    var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
    };

    return Results.Json(payload, options);
});

app.Run();

public class Person
{
    //Setting this property name explicitly means that the JsonSerializerOptions.PropertyNamingPolicy won't apply
    [JsonPropertyName("FullName")]
    public string Name { get; set; }

    public int Age { get; set; }

    public bool IsMarried { get; set; }

    public DateTimeOffset CurrentTime { get; set; }

    [JsonIgnore] // Do not serialize this property
    public bool? IsWorking { get; set; }

    public Dictionary<string, bool> Characters { get; set; }
}


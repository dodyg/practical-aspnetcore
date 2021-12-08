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
        IsWorking = true,
        Extensions = new Dictionary<string, object>
        {
            { "SuperPowers", new { Flight = false, Humor = true, Invisibility = true }}, // ad hoc object
            { "FavouriteWords", new string[] { "Hello", "Oh Dear", "Bye"} }, // an array of primitives
            { "Stats", new object[] { new { Flight = 0 }, new { Humor = 99 }, new { Invisibility = 30, Charged = true }}}, // an array of mixed objects
        }
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
    public string Name { get; set; }

    public int Age { get; set; }

    public bool IsMarried { get; set; }

    public DateTimeOffset CurrentTime { get; set; }

    public bool? IsWorking { get; set; }

    public Dictionary<string, bool> Characters { get; set; }

    public Dictionary<string, object> Extensions { get; set; }
}

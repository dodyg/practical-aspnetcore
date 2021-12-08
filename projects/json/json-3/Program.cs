using System.Text.Json;
using System.Text.Json.Serialization;

var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var payload = new
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
using System.Text.Json;
using System.Text.Json.Serialization;

var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var payload = new Person
    {
        Name = "Annie",
        TimeWaiting = new TimeSpan(1000, 0, 0, 0) // 1000 days
    };

    var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new TimeSpanConverter() }
    };

    return Results.Json(payload, options);
});

app.Run();

public class Person
{
    public string Name { get; set; }

    public TimeSpan TimeWaiting { get; set; }
}

public class TimeSpanConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeSpan.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

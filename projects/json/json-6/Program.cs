using System.Text.Json;
using Microsoft.Net.Http.Headers;

var app = WebApplication.Create();
app.MapGet("/", async context =>
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
        Superpowers = new List<Superpower> {
                new Superpower("Humor", 8),
                new Superpower("Intelligence", 10),
                new Superpower("Focus", 7)
        }
    };

    var options = new JsonWriterOptions
    {
        Indented = true
    };

    context.Response.Headers.Add(HeaderNames.ContentType, "application/json");

    await using (var writer = new Utf8JsonWriter(context.Response.Body, options))
    {
        writer.WriteStartObject();
        writer.WriteString("name", payload.Name);
        writer.WriteNumber("age", payload.Age);
        writer.WriteBoolean("isMarried", payload.IsMarried);
        writer.WriteString("currentTime", payload.CurrentTime);

        writer.WriteStartObject("characters");
        foreach (var kv in payload.Characters)
            writer.WriteBoolean(kv.Key, kv.Value);
        writer.WriteEndObject();

        writer.WriteEndObject();
    }
});

app.Run();

public class Superpower
{
    public string Name { get; set; }

    public short Rating { get; set; }

    public Superpower(string name, short rating)
    {
        Name = name;
        Rating = rating;
    }
}

public class Person
{
    public string Name { get; set; }

    public int Age { get; set; }

    public bool IsMarried { get; set; }

    public DateTimeOffset CurrentTime { get; set; }

    public Dictionary<string, bool> Characters { get; set; }

    public List<Superpower> Superpowers { get; set; }
}


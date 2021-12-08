using System.Text.Json.Nodes;

var app = WebApplication.Create();
app.Run(async context =>
{
    var objectNode = JsonNode.Parse(@"
    [
        {
            ""name"" : ""anne"",
            ""age"" : 34,
            ""gender"" : ""female""
        },
        {
            ""name"" : ""hadi"",
            ""age"" : 29,
            ""gender"" : ""non-binary"",
            ""favoriteNumbers"" : [1, 5, 6] 
        },
        {
            ""name"" : ""abdelfattah"",
            ""age"" : 30,
            ""gender"" : ""non-binary"",
            ""favoriteNumbers"" : [3, 9, 10, 11] 
        }
    ]");

    await context.Response.WriteAsync("From JsonNode.Parse\n");
    await context.Response.WriteAsync(objectNode.ToString());
    await context.Response.WriteAsync("\n\n");

    var verbose = new JsonArray()
    {
        new JsonObject()
        {
            ["name"] = JsonValue.Create("anne"),
            ["age"] = JsonValue.Create(34),
            ["gender"] = JsonValue.Create("female"),
            ["favoriteNumbers"] = new JsonArray()
            {
                JsonValue.Create(1),
                JsonValue.Create(2),
                JsonValue.Create(3)
            }
        },
        new JsonObject()
        {
            ["name"] = JsonValue.Create("hadi"),
            ["age"] = JsonValue.Create(29),
            ["gender"] = JsonValue.Create("non-binary"),
            ["favoriteNumbers"] = new JsonArray()
            {
                JsonValue.Create(1),
                JsonValue.Create(5),
                JsonValue.Create(6)
            }
        },
        new JsonObject()
        {
            ["name"] = JsonValue.Create("abdelfattah"),
            ["age"] = JsonValue.Create(30),
            ["gender"] = JsonValue.Create("non-binary"),
            ["favoriteNumbers"] = new JsonArray()
            {
                JsonValue.Create(3),
                JsonValue.Create(9),
                JsonValue.Create(10),
                JsonValue.Create(11)
            }
        }
    };

    await context.Response.WriteAsync("From new JsonObject verbose\n");
    await context.Response.WriteAsync(verbose.ToString());

    var terse = new JsonArray()
    {
        new JsonObject()
        {
            ["name"] = "anne",
            ["age"] = 34,
            ["gender"] = "female",
            ["favoriteNumbers"] = new JsonArray(1, 2, 3)
        },
        new JsonObject()
        {
            ["name"] = "hadi",
            ["age"] = 29,
            ["gender"] = "non-binary",
            ["favoriteNumbers"] = new JsonArray(1, 5, 6)
        },
        new JsonObject()
        {
            ["name"] = "abdelfattah",
            ["age"] = 30,
            ["gender"] = "non-binary",
            ["favoriteNumbers"] = new JsonArray(3, 9, 10, 11)
        }
    };

    await context.Response.WriteAsync("\n\nFrom new JsonObject terse\n");
    await context.Response.WriteAsync(terse.ToString());
});

app.Run();
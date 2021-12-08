using System.Text.Json.Nodes;

var app = WebApplication.Create();
app.Run(async context =>
{
    var objectNode = JsonNode.Parse(@"
    {
        ""name"" : ""anne"",
        ""age"" : 34,
        ""gender"" : ""female"",
        ""favouriteNumbers"" : [ 1, 2, 3]
    }");

    await context.Response.WriteAsync("From JsonNode.Parse\n");
    await context.Response.WriteAsync(objectNode.ToString());
    await context.Response.WriteAsync("\n\n");

    var verbose = new JsonObject()
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
    };

    await context.Response.WriteAsync("From new JsonObject verbose\n");
    await context.Response.WriteAsync(verbose.ToString());

    var terse = new JsonObject()
    {
        ["name"] = "anne",
        ["age"] = 34,
        ["gender"] = "female",
        ["favoriteNumbers"] = new JsonArray()
        {
            1, 2, 3
        }
    };

    await context.Response.WriteAsync("\n\nFrom new JsonObject terse\n");
    await context.Response.WriteAsync(terse.ToString());
});

app.Run();
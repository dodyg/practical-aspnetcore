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

    var additionalNode = JsonNode.Parse(@"
    {
            ""name"" : ""prince"",
            ""age"" : 30,
            ""gender"" : ""male""
    }
    ");

    var objectArray = objectNode.AsArray();
    objectArray.Insert(objectArray.Count, additionalNode);

    var firstNode = JsonNode.Parse(@"
    {
            ""name"" : ""arathi"",
            ""age"" : 33,
            ""gender"" : ""female""
    }
    ");

    objectArray.Insert(0, firstNode);

    await context.Response.WriteAsync("Updated document\n");
    await context.Response.WriteAsync(objectNode.ToString());
    await context.Response.WriteAsync("\n\n");
});

app.Run();
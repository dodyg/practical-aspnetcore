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

    await context.Response.WriteAsync(objectNode.ToString());
    await context.Response.WriteAsync("\n");
    await context.Response.WriteAsync($"Path: {objectNode.GetPath()}");

    await context.Response.WriteAsync("\n\nNow lets find the non-binary person who has three `favoriteNumbers`\n");

    var hadi = objectNode.AsArray().Where(x => x["gender"].GetValue<string>() == "non-binary" && x["favoriteNumbers"].AsArray().Count == 3).FirstOrDefault();

    if (hadi is object)
    {
        await context.Response.WriteAsync("\n");
        await context.Response.WriteAsync(hadi.ToString());
        await context.Response.WriteAsync($"\nPath : {hadi.GetPath()}");
    }    
});

app.Run();
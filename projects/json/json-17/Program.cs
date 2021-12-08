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

    await context.Response.WriteAsync("\n\nNow lets find the person whose 3rd `favoriteNumbers` is 10\n");

    var abdelfattah = objectNode.AsArray().Where(x => x["favoriteNumbers"]?[2]?.GetValue<int>() == 10).FirstOrDefault();

    if (abdelfattah is object)
    {
        await context.Response.WriteAsync("\n");
        await context.Response.WriteAsync(abdelfattah.ToString());
        await context.Response.WriteAsync($"\nPath : {abdelfattah.GetPath()}");
    }    
});

app.Run();
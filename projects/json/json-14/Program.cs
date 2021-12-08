using System.Text.Json.Nodes;

var app = WebApplication.Create();
app.Run(async context =>
{
    var objectNode = JsonNode.Parse(@"
    [
        {
            ""name"" : ""anne"",
            ""age"" : 34
        },
        {
            ""name"" : ""hadi"",
            ""age"" : 29
        },
        {
            ""name"" : ""abdelfattah"",
            ""age"" : 30
        }
    ]");

    await context.Response.WriteAsync(objectNode.ToString());
    await context.Response.WriteAsync("\n");
    await context.Response.WriteAsync($"Path: {objectNode.GetPath()}");
    await context.Response.WriteAsync("\n\n\nNow let's find an object with age value of 29\n");

    var hadi = objectNode.AsArray().Where(x => x["age"].GetValue<int>() == 29).FirstOrDefault();
    if (hadi is object)
    {
        await context.Response.WriteAsync("\n");
        await context.Response.WriteAsync(hadi.ToString());
        await context.Response.WriteAsync($"\nPath : {hadi.GetPath()}");
    }
});

app.Run();
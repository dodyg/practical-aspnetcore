using System.Text.Json.Nodes;

WebApplication app = WebApplication.Create();

app.Run(async context =>
{
    var numberNode = JsonNode.Parse(@"{""age"" : 34 }");
    int age = (int) numberNode["age"];
    await context.Response.WriteAsync($"{numberNode} \n");
    await context.Response.WriteAsync($"Value of age is {age}\n\n\n");
    
    var stringNode = JsonNode.Parse(@"{""name"" : ""anne""}");
    string name = (string) stringNode["name"];
    await context.Response.WriteAsync($"{stringNode} \n");
    await context.Response.WriteAsync($"Value of name is {name}\n\n\n");

    var numberArrayNode = JsonNode.Parse("[2, 4, 6]");
    await context.Response.WriteAsync($"{numberArrayNode}\n");
    int item0 = (int) numberArrayNode[0];
    int item1 = (int) numberArrayNode[1];
    int item2 = (int) numberArrayNode[2];
    await context.Response.WriteAsync($"Values of array: {item0}, {item1}, {item2}\n\n");

    //alternatively
    JsonArray childrenAge = numberArrayNode.AsArray();
    await context.Response.WriteAsync($"Size of array: {childrenAge.Count}\nValues of array: ");
    foreach(JsonNode r in childrenAge)
    {
        await context.Response.WriteAsync($"{r}, ");
    }
});

app.Run();
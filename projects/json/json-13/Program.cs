using System.Text.Json.Nodes;

var app = WebApplication.Create();

app.Run(async context =>
{
    var objectNode = JsonNode.Parse(@"
    {
        ""person"" : {
            ""name"" : ""anne"",
            ""age"" : 34 ,
            ""favoriteNumbers"" : [2, 4, 6]
        }
    }");
    
    await context.Response.WriteAsync($"{objectNode} \n");

    var person = objectNode["person"];
    int age = (int) person["age"];
    string name = (string) person["name"];
    int[] favoriteNumbers = person["favoriteNumbers"].AsArray().Select(x => (int) x).ToArray();
    
    await context.Response.WriteAsync($"name : {name}\n");
    await context.Response.WriteAsync($"age : {age}\n");
    await context.Response.WriteAsync($"favorite numbers : {string.Join(",", favoriteNumbers)}\n\n");

    await context.Response.WriteAsync("Now we are using JsonObject\n");
    JsonObject personObject = person.AsObject();
    await context.Response.WriteAsync($"Number of elements in the object: {personObject.Count}\n\n");
    foreach(var el in personObject)
    {
        await context.Response.WriteAsync($"{el.Key} => {el.Value}\n");
    }
});

app.Run();
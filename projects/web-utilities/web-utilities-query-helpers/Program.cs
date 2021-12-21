using Microsoft.AspNetCore.WebUtilities;

var app = WebApplication.Create();

var arguments = new Dictionary<string, string>()
{
    {"greetings", "hello-world"},
    {"origin", "cairo"}
};

var path = QueryHelpers.AddQueryString("/greet", arguments);
var path2 = QueryHelpers.AddQueryString(path, "name", "annie");

app.Run(async context =>
{
    await context.Response.WriteAsync($"{path}\n{path2}");
});

app.Run();
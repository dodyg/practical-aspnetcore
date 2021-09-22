using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var app = WebApplication.Create(args);

app.Urls.Add("http://localhost:3000");
app.Urls.Add("http://localhost:5000");

app.Run(async (context) =>
{
    foreach(var u in app.Urls)
        await context.Response.WriteAsync(u + "\n");
});

await app.RunAsync();
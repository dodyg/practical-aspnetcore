using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var app = WebApplication.Create();
app.Run(async (context) =>
{
    foreach(var u in app.Urls)
        await context.Response.WriteAsync(u + "\n");
});

await app.RunAsync("http://localhost:3000");
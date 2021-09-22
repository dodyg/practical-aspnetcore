using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var app = WebApplication.Create(args);

app.Run(async (context) =>
{
    foreach(var u in app.Urls)
        await context.Response.WriteAsync(u + "\n");
});

app.Run();
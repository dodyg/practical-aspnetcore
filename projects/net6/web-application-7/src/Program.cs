using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;

var app = WebApplication.Create();
app.Run(async (context) =>
{
    foreach(var e in app.Configuration.AsEnumerable())
    {
        await context.Response.WriteAsync($"{e.Key} = {e.Value}\n");
    }
});

await app.RunAsync();
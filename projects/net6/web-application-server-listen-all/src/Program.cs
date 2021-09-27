using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var app = WebApplication.Create(args);

//app.Urls.Add("http://*:3000");
//app.Urls.Add("http://+:3000"); //You can also use this. They are equivalent: Bind to all IP4 and IP6 addresses.
app.Urls.Add("http://0.0.0.0:3000"); //You can also use this. This means bind to all IP4 addresses

app.Run(async (context) =>
{
    foreach(var u in app.Urls)
        await context.Response.WriteAsync(u + "\n");
});

await app.RunAsync();
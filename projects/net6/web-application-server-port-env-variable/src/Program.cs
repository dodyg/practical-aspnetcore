using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var app = WebApplication.Create(args);
var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

app.Run(async (context) =>
{
    foreach(var u in app.Urls)
        await context.Response.WriteAsync(u + "\n");
});

app.Run($"http://localhost:{port}");
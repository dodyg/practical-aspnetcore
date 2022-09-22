var builder = WebApplication.CreateBuilder();

builder.WebHost.ConfigureKestrel(k =>
{
    k.ListenLocalhost(8111);
    k.ListenLocalhost(8112); 
});

var app = builder.Build();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("hello world from http://localhost:8111");
}).RequireHost("localhost:8111");

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("hello world from http://localhost:8112");
}).RequireHost("localhost:8112");

app.Run();

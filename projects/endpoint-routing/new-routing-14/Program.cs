using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapGet("/", async context =>
{
    var feature = context.Features.Get<IEndpointFeature>();
    await context.Response.WriteAsync($"Endpoint Name {feature.Endpoint.DisplayName}");
});

app.Run();
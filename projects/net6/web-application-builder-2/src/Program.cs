using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder();
builder.Services.AddRazorPages();

var app = builder.Build();
app.MapRazorPages();

await app.RunAsync();

using Microsoft.Extensions.DependencyInjection;
using ComponentEvents;

var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AppState>();

var app = builder.Build();

app.UseStaticFiles();

app.MapBlazorHub();
app.MapRazorPages();
app.MapFallbackToPage("/Index");

app.Run();
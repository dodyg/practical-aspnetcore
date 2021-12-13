using DependencyInjection.Services;

var builder = WebApplication.CreateBuilder();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<TheTransientClock>();
builder.Services.AddSingleton<TheSingletonClock>();
builder.Services.AddScoped<TheScopedClock>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapRazorPages();
app.MapFallbackToPage("/Index");
app.MapBlazorHub();

app.Run();

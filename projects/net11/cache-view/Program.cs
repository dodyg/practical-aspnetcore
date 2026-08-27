var builder = WebApplication.CreateBuilder();

builder.Services.AddRazorComponents();

// When HybridCache is registered, CacheView picks it up automatically and
// gets a two-tier local/distributed cache.
builder.Services.AddHybridCache();

var app = builder.Build();

app.MapRazorComponents<CacheView.App>();
app.Run();

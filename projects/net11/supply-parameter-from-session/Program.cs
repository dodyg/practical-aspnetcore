var builder = WebApplication.CreateBuilder();

builder.Services.AddRazorComponents();
builder.Services.AddAntiforgery();

// [SupplyParameterFromSession] needs the HTTP session services + middleware.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

app.UseAntiforgery();
app.UseSession();
app.MapRazorComponents<SupplyParameterFromSession.App>();
app.Run();

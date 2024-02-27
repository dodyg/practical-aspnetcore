var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddAntiforgery();

var app = builder.Build();
app.UseAntiforgery();
app.MapRazorComponents<RazorComponentSeven.App>()
    .AddInteractiveServerRenderMode();
app.Run();



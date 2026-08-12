var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddAntiforgery();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapStaticAssets();
app.UseAntiforgery();
app.MapRazorComponents<RazorComponentTen.App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();

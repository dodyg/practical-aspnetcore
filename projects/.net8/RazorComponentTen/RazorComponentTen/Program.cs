var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddServerComponents()
    .AddWebAssemblyComponents();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapRazorComponents<RazorComponentTen.App>()
    .AddServerRenderMode()
    .AddWebAssemblyRenderMode();

app.Run();

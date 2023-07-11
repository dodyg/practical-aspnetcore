var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorComponents()
    .AddServerComponents()
    .AddWebAssemblyComponents();

var app = builder.Build();
app.MapRazorComponents<RazorComponentEight.App>()
    .AddWebAssemblyRenderMode();    
app.Run();
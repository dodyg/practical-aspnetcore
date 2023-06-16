var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorComponents()
    .AddServerComponents();

var app = builder.Build();
app.MapRazorComponents<RazorComponentSeven.App>();
app.Run();



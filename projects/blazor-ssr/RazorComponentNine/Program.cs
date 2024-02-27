var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorComponents();

var app = builder.Build();
app.MapRazorComponents<RazorComponentNine.App>();
app.Run();



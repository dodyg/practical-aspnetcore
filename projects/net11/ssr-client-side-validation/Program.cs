var builder = WebApplication.CreateBuilder();
builder.WebHost.UseStaticWebAssets();
builder.Services.AddRazorComponents();
builder.Services.AddAntiforgery();

var app = builder.Build();

app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<SSRClientSideValidation.App>();
app.Run();

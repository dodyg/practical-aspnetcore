using Microsoft.AspNetCore.Components.Endpoints;

var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorComponents(options =>
{
    // The default cookie provider cannot write TempData after a redirect response
    // has started, so use the session-storage provider instead.
    options.TempDataProviderType = TempDataProviderType.SessionStorage;
});
builder.Services.AddAntiforgery();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

app.UseAntiforgery();
app.UseSession();
app.MapRazorComponents<SupplyParameterFromTempData.App>();
app.Run();

var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorComponents();
builder.Services.AddAntiforgery();

var app = builder.Build();
app.UseAntiforgery();
app.MapRazorComponents<RazorFormHandlingFour.Pages.App>();
app.Run();



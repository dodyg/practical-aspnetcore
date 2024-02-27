var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddAntiforgery();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<RazorComponentThirteen.App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Wasm.Pages.Index).Assembly);

app.Run();

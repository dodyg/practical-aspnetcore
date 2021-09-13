using Microsoft.AspNetCore.Builder;
WebApplication app = WebApplication.Create();
app.UseWelcomePage();
await app.RunAsync();
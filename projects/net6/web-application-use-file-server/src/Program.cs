using Microsoft.AspNetCore.Builder;
var app = WebApplication.Create();
app.UseFileServer();
await app.RunAsync();
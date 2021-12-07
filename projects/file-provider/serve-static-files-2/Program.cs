var app = WebApplication.Create();
app.UseStaticFiles();
app.UseDirectoryBrowser();
app.Run();
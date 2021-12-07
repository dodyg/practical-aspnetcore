var app = WebApplication.Create();
app.UseFileServer(enableDirectoryBrowsing: true);
app.Run();
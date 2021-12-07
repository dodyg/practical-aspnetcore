var app = WebApplication.Create();
app.UseStaticFiles();
app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    RequestPath = new PathString("/browse")
});

app.Run(async context =>
{
    await context.Response.WriteAsync("<html><body><a href=\"browse\">browse directory</a></body></html>");
});

app.Run();
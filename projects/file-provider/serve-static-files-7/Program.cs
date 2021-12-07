using Microsoft.Extensions.FileProviders;

var app = WebApplication.Create();
app.UseStaticFiles(); // By default this will server files out of wwwroot folder
app.UseStaticFiles(new StaticFileOptions()
{
    //The PhysialFileProvider will take any valid path. This way you can 
    //specify which folders will server the static files
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot2")),
    RequestPath = new PathString("/2")
});

app.Run(async context =>
{
    context.Response.Headers.Add("content-type", "text/html");

    await context.Response.WriteAsync(@"
                <html>
                <body>
                    From wwwroot</br>
                    <img src=""/kitty1.jpg""/>
                    <hr/>
                    From wwwroot2</br>
                    <img src=""/2/kitty2.jpg"" />
                </body>
                </html>
                ");
});

app.Run();
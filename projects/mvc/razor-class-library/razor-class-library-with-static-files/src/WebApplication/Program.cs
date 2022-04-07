var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorPages();
builder.Services.ConfigureOptions(typeof(RazorClassLibrary1.UiConfigureOptions));
builder.Services.ConfigureOptions(typeof(RazorClassLibrary2.UiConfigureOptions));

var app = builder.Build();
app.UseRouting();
app.UseStaticFiles();
app.MapRazorPages();

app.Run(async (context) =>
{
    context.Response.Headers.Add("Content-Type", "text/html");

    await context.Response.WriteAsync(@"<html>
        <body>
        <h1>Razor Class Library sample with static files (image, css, js)</h1>
        Visit page from <a href=""/module1"">RazorClassLibrary1</a> and <a href=""/module2"">RazorClassLibrary2</a>.
        </body>
        </html>");
});

app.Run();
var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorPages();
    
var app = builder.Build();
app.MapRazorPages();

app.Run(async (context) =>
{
    context.Response.Headers.Add("Content-Type", "text/html");

    await context.Response.WriteAsync(@"<html>
        <body>
        <h1>Hello World from the Web Application!</h1>
        Visit page from <a href=""/module1"">RazorClassLibrary1</a> and <a href=""/module2"">RazorClassLibrary2</a>.
        </body>
        </html>");
});

app.Run();
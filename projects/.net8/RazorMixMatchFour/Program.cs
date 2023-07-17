var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();
builder.Services.AddMvc();
builder.Services.AddRazorPages();

var app = builder.Build();

app.MapGet("/", () => 
{
    return Results.Text("""
    <html>
        <body>
            Hello world. This is from Minimal API.
            <ul>
                <li><a href="/index-2">MVC is here</a></li>
                <li><a href="/index-3">Razor Pages is here</a></li>
                <li><a href="/blazor-ssr">Blazor SSR is here</a></li>
            </ul>
        </body>
    </html>
    """, "text/html");
});

app.MapControllers();
app.MapRazorPages();
app.MapRazorComponents<RazorMixMatchOne.App>();

app.Run();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();
builder.Services.AddAntiforgery();

var app = builder.Build();
app.UseAntiforgery();

app.MapGet("/", () => 
{
    return Results.Text("""
    <html>
        <body>
            Hello world. This is from Minimal API.
            <br/>
            <a href="/blazor-ssr">Blazor SSR is here</a>
        </body>
    </html>
    """, "text/html");
});

app.MapRazorComponents<RazorMixMatchThree.App>();

app.Run();

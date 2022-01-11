var builder = WebApplication.CreateBuilder();
builder.Services.AddHealthChecks();
var app = builder.Build();

app.MapHealthChecks("/WhatsUp");
app.MapGet("/", async context =>
{
    await context.Response.WriteAsync(@"
        <html>
            <body>
                <h1>Health Check</h1>
                The health check service checks on this url <a href=""/WhatsUp"">/WhatsUp</a>. 
            </body>
        </html> 
        ");
});

app.Run();
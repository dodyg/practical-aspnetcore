using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder();
builder.WebHost.ConfigureKestrel(k =>
{
    k.ConfigureEndpointDefaults(e =>
    {
        e.Protocols = HttpProtocols.Http2;
    });
}   
);

var app = builder.Build();
app.MapGet("/", async context =>
{
    await context.Response.WriteAsync(@"
        <html>
            <body>
                This endpoint runs on HTTP/2. It is only accessible from the browser over HTTPS connection. 
            </body>
        </html> 
        ");
});

app.Run();

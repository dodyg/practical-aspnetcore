var builder = WebApplication.CreateBuilder();
builder.Services.AddHttpClient();

var app = builder.Build();

app.Run(async context =>
{
    var httpClient = context.RequestServices.GetService<IHttpClientFactory>();
    var client = httpClient.CreateClient();
    var result = await client.GetStringAsync("http://scripting.com/rss.xml");

    context.Response.Headers.Add("Content-Type", "application/rss+xml");
    await context.Response.WriteAsync(result);
});

app.Run();
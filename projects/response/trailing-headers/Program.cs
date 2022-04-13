using System.Diagnostics;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

var builder = WebApplication.CreateBuilder();
builder.Services.AddHttpClient();
builder.WebHost.ConfigureKestrel(k =>
    k.Listen(IPAddress.Any, 5000, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
        listenOptions.UseHttps();
    })
);

var app = builder.Build();

//We need this switch because we are connecting to an unsecure server. If the server runs on SSL, there's no need for this switch.
app.Run(async context =>
{
    bool supportTrailers = context.Response.SupportsTrailers();

    Stopwatch watch = null;

    if (supportTrailers)
    {
        context.Response.DeclareTrailer("Server-Timing");
        watch = new Stopwatch();
    }

    watch?.Start();

    var httpFactory = context.RequestServices.GetService<IHttpClientFactory>();
    var httpClient = httpFactory.CreateClient();

    using var response = await httpClient.GetStreamAsync("http://histo.io/");

    context.Response.Headers.Add("Content-Type", "text/html");
    await response.CopyToAsync(context.Response.Body);

    watch?.Stop();
    if (supportTrailers)
    {
        Console.WriteLine("Server-Timing " + watch.ElapsedMilliseconds + " ms.");
                //You won't be able to see this in any browser dev tools
        context.Response.AppendTrailer("Server-Timing", $"practical-aspnet-core;dur={watch.ElapsedMilliseconds}.0");
    }
});

app.Run();
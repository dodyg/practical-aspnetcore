using System.Text;
using Microsoft.AspNetCore.Http.Features;

var app = WebApplication.Create();
app.Run(async context =>
{
    context.Response.Headers.Add("Content-Type", "text/html; charset=utf-8");
    var feature = context.Features.Get<IHttpResponseBodyFeature>();

    if (feature != null)
    {
        await feature.StartAsync();

        await feature.Stream
            .WriteAsync(Encoding.UTF8.GetBytes("<html><body>Hello 🌍</body></html>"));

        await feature.CompleteAsync();
    }

});

app.Run();

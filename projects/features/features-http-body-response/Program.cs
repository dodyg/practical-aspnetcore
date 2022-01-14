using Microsoft.AspNetCore.Http.Features;
using System.Text;

var app = WebApplication.Create();
app.Run(async context =>
{
    context.Response.Headers.Add("Content-Type", "text/html; charset=utf-8");
    var feature = context.Features.Get<IHttpResponseBodyFeature>();
    await feature.StartAsync();
    await feature.Stream.WriteAsync(Encoding.UTF8.GetBytes("<html><body style=\"font-size:240px;text-align:center;\">Hello 🌍</body></html>"));
    await feature.CompleteAsync();
});
app.Run();
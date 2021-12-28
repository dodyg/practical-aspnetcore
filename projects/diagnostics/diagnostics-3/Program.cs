using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Diagnostics;

var app = WebApplication.Create();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "text/html";

        var feature = context.Features.Get<IExceptionHandlerFeature>();

        if (feature != null)
        {
            await context.Response.WriteAsync($"<h1>Custom Error Page</h1> {HtmlEncoder.Default.Encode(feature.Error.Message)}");
            await context.Response.WriteAsync($"<hr />{HtmlEncoder.Default.Encode(feature.Error.Source)}");
        }
    });
});

//trigger exception
app.Run(context => throw new Exception("Hello World Exception"));
app.Run();
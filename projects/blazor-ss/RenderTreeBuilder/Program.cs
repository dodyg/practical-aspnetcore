using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;

// converts an IHtmlContent to a string
static string GetString(IHtmlContent htmlContent)
{
    using (var writer = new StringWriter())
    {
        htmlContent.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(); // for IHtmlHelper
builder.Services.AddServerSideBlazor();

var app = builder.Build();

app.UseStaticFiles();
app.MapBlazorHub();

app.MapGet("/",
    async (HttpContext ctx) =>
    {
        ctx.Response.ContentType = "text/html";

        var htmlHelper = ctx.RequestServices.GetRequiredService<IHtmlHelper>();
        ((IViewContextAware)htmlHelper).Contextualize(new ViewContext { HttpContext = ctx });

                // all of this could be done with a Razor Page,
                // but this sample uses C# instead

                await ctx.Response.WriteAsync("<html lang=\"en\">");

        await ctx.Response.WriteAsync("<head>");
        await ctx.Response.WriteAsync("<meta charset=\"utf-8\" />");
        await ctx.Response.WriteAsync("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />");
        await ctx.Response.WriteAsync("<base href=\"/\" />");
        await ctx.Response.WriteAsync("</head>");

        await ctx.Response.WriteAsync("<body>");

        await ctx.Response.WriteAsync("<div>");
        {
            var htmlContent = await htmlHelper.RenderComponentAsync<ListNames>(RenderMode.Server);
            await ctx.Response.WriteAsync(GetString(htmlContent));
        }
        await ctx.Response.WriteAsync("</div>");

        await ctx.Response.WriteAsync("<script src=\"_framework/blazor.server.js\"></script>");

        await ctx.Response.WriteAsync("</body>");
        await ctx.Response.WriteAsync("</html>");
    }
    );

app.Run();


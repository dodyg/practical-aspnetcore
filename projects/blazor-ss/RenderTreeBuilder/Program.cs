using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Endpoints;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Html;

// converts an IHtmlAsyncContent to a string.
// must run on the renderer's dispatcher
async Task<string> GetStringAsync(IHtmlAsyncContent htmlContent)
{
    using (var writer = new StringWriter())
    {
        await htmlContent.WriteToAsync(writer);
        return writer.ToString();
    }
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServerSideBlazor();
builder.Services.AddRazorComponents(); // registers IComponentPrerenderer

var app = builder.Build();

app.UseStaticFiles();
app.MapBlazorHub();

app.MapGet("/",
    async (HttpContext ctx) =>
    {
        ctx.Response.ContentType = "text/html";

        var prerenderer = ctx.RequestServices.GetRequiredService<IComponentPrerenderer>();

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
            var htmlContent = await prerenderer.PrerenderComponentAsync(ctx, typeof(ListNames), RenderMode.InteractiveServer, ParameterView.Empty);

            // the prerendered html must be written on the renderer's dispatcher
            var htmlString = await prerenderer.Dispatcher.InvokeAsync(() => GetStringAsync(htmlContent));
            await ctx.Response.WriteAsync(htmlString);
        }
        await ctx.Response.WriteAsync("</div>");

        await ctx.Response.WriteAsync("<script src=\"_framework/blazor.server.js\"></script>");

        await ctx.Response.WriteAsync("</body>");
        await ctx.Response.WriteAsync("</html>");
    }
    );

app.Run();

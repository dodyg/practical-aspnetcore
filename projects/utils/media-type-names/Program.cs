using System.Reflection;
using System.Net.Mime;
using Microsoft.Net.Http.Headers;

var app = WebApplication.Create();

static List<FieldInfo> GetConstants(Type type)
{
    FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

    return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
}

app.Run(async context =>
{
    context.Response.Headers.Add(HeaderNames.ContentType, MediaTypeNames.Text.Html);

    await context.Response.WriteAsync("<html><body>");

    await context.Response.WriteAsync("<h1>System.Net.Mime.MediaTypeNames</h1>");

    await context.Response.WriteAsync("<h2>MediaTypeNames.Application</h2>");
    await context.Response.WriteAsync("<ul>");
    foreach (var h in GetConstants(typeof(MediaTypeNames.Application)))
    {
        await context.Response.WriteAsync($"<li>{h.Name} = {h.GetValue(h)}</li>");
    }
    await context.Response.WriteAsync("</ul>");

    await context.Response.WriteAsync("<h2>MediaTypeNames.Text</h2>");
    await context.Response.WriteAsync("<ul>");
    foreach (var h in GetConstants(typeof(MediaTypeNames.Text)))
    {
        await context.Response.WriteAsync($"<li>{h.Name} = {h.GetValue(h)}</li>");
    }
    await context.Response.WriteAsync("</ul>");

    await context.Response.WriteAsync("<h2>MediaTypeNames.Image</h2>");
    await context.Response.WriteAsync("<ul>");
    foreach (var h in GetConstants(typeof(MediaTypeNames.Image)))
    {
        await context.Response.WriteAsync($"<li>{h.Name} = {h.GetValue(h)}</li>");
    }
    await context.Response.WriteAsync("</ul>");

    await context.Response.WriteAsync("</body></html>");

});

app.Run();
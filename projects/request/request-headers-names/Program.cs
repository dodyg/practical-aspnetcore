using Microsoft.Net.Http.Headers;
using System.Reflection;

List<FieldInfo> GetConstants(Type type)
{
    FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

    return fieldInfos.ToList();
}

var app = WebApplication.Create();

app.Run(async context =>
{
    context.Response.Headers.Add(HeaderNames.ContentType, "text/html");

    await context.Response.WriteAsync("<html><body>");
    await context.Response.WriteAsync("<h1>Request Headers from Microsoft.Net.Http.Headers.HeaderNames</h1>");
    await context.Response.WriteAsync("<ul>");
    foreach (var h in GetConstants(typeof(HeaderNames)))
    {
        await context.Response.WriteAsync($"<li>{h.Name} = {h.GetValue(h)}</li>");
    }
    await context.Response.WriteAsync("</ul>");
    await context.Response.WriteAsync("</body></html>");
});

app.Run();
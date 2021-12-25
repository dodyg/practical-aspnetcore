using System.Reflection;
using System.Net;

var app = WebApplication.Create();

static List<FieldInfo> GetConstants(Type type)
{
    FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

    return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
}

app.Run(async context =>
{
    context.Response.Headers.Add("Content-Type", "text/html");
    await context.Response.WriteAsync(@"<html>
                <head>
                    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/bulma/0.7.5/css/bulma.css"" />
                </head>
                <body class=""content"">
                <h1>Battle of the Http Status Codes</h1>
                ");

    await context.Response.WriteAsync(@"<table class=""table"">
                <thead>
                    <tr>
                        <th>Microsoft.AspNetCore.Http.StatusCodes</th>
                        <th>System.Net.HttpStatusCode</th>
                    </tr>
                </thead>");
    await context.Response.WriteAsync("<tbody><tr><td><ul>");
    foreach (var code in GetConstants(typeof(StatusCodes)))
    {
        await context.Response.WriteAsync($"<li>{code.Name} = {code.GetValue(code)}</li> \n");
    }

    await context.Response.WriteAsync("</ul></td><td><ul>");

    foreach (var code in Enum.GetNames(typeof(HttpStatusCode)))
    {
        var status = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), code);
        await context.Response.WriteAsync($"<li>{code} = {(int)status}</li> \n");
    }

    await context.Response.WriteAsync("</ul></td></tr></tbody></table>");
    await context.Response.WriteAsync(@"</body></html>");
});

app.Run();
var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapMethods("", new[] { "GET" }, async context =>
{
    var content =
@"<html>
<body>
<h1>Hello World</h1>
<form method=""post"" action=""/about"">
<input type=""submit"" value=""POST"" />
</form>
</body>
</html>";
    await context.Response.WriteAsync(content);

});

app.MapMethods("about", new[] { "POST" }, async context =>
{
    var content =
@"<html>
<body>
<h1>This page only supports POST method</h1>
<p>
If you try to retrieve this page directly, you will see nothing.
</p>
</body>
</html>";
    await context.Response.WriteAsync(content);

});

app.Run();
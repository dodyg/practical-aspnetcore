var app = WebApplication.Create();

app.MapWhen(context => context.Request.Path.Value.Contains("/hello"),
(IApplicationBuilder pp) =>
{
    //nested
    app.Map("/world", (IApplicationBuilder ppa) => ppa.Run(context => 
        context.Response.WriteAsync($"Path: {context.Request.Path} - Path Base: {context.Request.PathBase}")));

    pp.Run(context =>
        context.Response.WriteAsync($"Path: {context.Request.Path} - Path Base: {context.Request.PathBase}"));
});

app.Run(context =>
{
    context.Response.Headers.Add("content-type", "text/html");
    return context.Response.WriteAsync(@"<a href=""/hello"">/hello</a> <a href=""/hello/world"">/hello/world</a>");
});

app.Run();
var app = WebApplication.Create();

app.Map("/hello", (IApplicationBuilder pp) => pp.Run(context => context.Response.WriteAsync("/hello path")));
app.Map("/world", (IApplicationBuilder pp) => pp.Run(context => context.Response.WriteAsync("/world path")));

app.Run(context =>
{
    context.Response.Headers.Add("Content-Type", "text/html");
    return context.Response.WriteAsync(@"
                   <a href=""/hello"">hello</a> <a href=""/world"">world</a>
                ");
});

app.Run();
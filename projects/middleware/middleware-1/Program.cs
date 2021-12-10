var app = WebApplication.Create();

//The order of these things are important. 
//Only one Middleware should write to the Response. 
//Do not write to Response before next.Invoke()
app.Use(async (context, next) =>
{
    context.Items["Greeting"] = "Hello World";
    await next(context);
    await context.Response.WriteAsync($"{context.Items["Greeting"]}\n");
    await context.Response.WriteAsync($"{context.Items["Goodbye"]}\n");
});

app.Use(async(context, next) =>
{
    context.Items["Goodbye"] = "Goodbye for now";
    await next(context);
});

app.Run();
var app = WebApplication.Create();

//The order of these things are important. 
app.Use(async (context, next) =>
{
    context.Items["Content"] += "[1] ----- \n";//1
    await next(context);
    context.Items["Content"] += "[5] =====\n";//5
    await context.Response.WriteAsync(context.Items["Content"] as string);
});

app.Use(async (context, next) =>
{
    context.Items["Content"] += "[2] Hello world \n";//2
    await next(context);
    context.Items["Content"] += "[4]  \n";//4
});

app.Run(context =>
{
    context.Items["Content"] += "[3] ----- \n";//3
    return Task.CompletedTask;
});

app.Run();
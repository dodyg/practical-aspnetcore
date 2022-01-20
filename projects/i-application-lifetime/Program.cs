var app = WebApplication.Create();

var lifetime = app.Services.GetService<IHostApplicationLifetime>();
lifetime.ApplicationStarted.Register(() => System.Console.WriteLine("===== Server is starting"));
lifetime.ApplicationStopping.Register(() => System.Console.WriteLine("===== Server is stopping"));
lifetime.ApplicationStopped.Register(() => System.Console.WriteLine("===== Server has stopped"));

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello world");
});

app.Run();
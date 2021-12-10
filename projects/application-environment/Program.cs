var app = WebApplication.Create();
app.Run(async context =>
{
    context.Response.Headers.Add("content-type", "text/html");
    await context.Response.WriteAsync($"Application Name: {System.Reflection.Assembly.GetEntryAssembly().GetName().Name}<br/>");
    await context.Response.WriteAsync($"Application Base Path: {System.AppContext.BaseDirectory}<br/>");

    System.Reflection.Assembly entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
    var targetFramework = entryAssembly.GetCustomAttributes(typeof(System.Runtime.Versioning.TargetFrameworkAttribute), true)[0] as System.Runtime.Versioning.TargetFrameworkAttribute;
    await context.Response.WriteAsync($"Target Framework: {targetFramework.FrameworkName}<br/>");

    await context.Response.WriteAsync($"Version: {System.Reflection.Assembly.GetEntryAssembly().GetName().Version}<br/>");
});
app.Run();

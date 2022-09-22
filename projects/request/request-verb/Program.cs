var app = WebApplication.Create();
app.Run(context => context.Response.WriteAsync($"Request {context.Request.Method}"));
app.Run();

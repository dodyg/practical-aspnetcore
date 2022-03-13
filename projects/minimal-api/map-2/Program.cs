IResult TryContext(HttpContext context) => Results.Json(new { path = context.Request.Path });

var app = WebApplication.Create();
app.Map("/", TryContext);
app.Run();

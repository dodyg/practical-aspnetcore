using static RouteSpy.RouteDebugger;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<RouteDebuggerMiddleware>();
builder.Services.AddMvc();

var app = builder.Build();

app.UseRouteDebugger();

app.MapGet("/", () => "Hello World!");

app.Run();

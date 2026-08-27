var app = WebApplication.Create(args);

// Marker middleware: if this header is present on a response, the endpoint ran
// the full middleware pipeline instead of short-circuiting.
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Middleware-Ran"] = "true";
    await next(context);
});

// Runs immediately after routing — the middleware above is skipped.
app.MapGet("/health", [ShortCircuit] () => Results.Text("Healthy"));

// Runs the full pipeline (note the X-Middleware-Ran header).
app.MapGet("/", () => "This endpoint ran the full pipeline.");

app.Run();

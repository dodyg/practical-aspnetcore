# HX-Trigger Response Header

This example demonstrates the usage of `HX-Trigger` response header to emit custom events at the client([doc](https://htmx.org/headers/hx-trigger/)).

```csharp
htmx.MapGet("/", (HttpRequest request, HttpResponse response) =>
{
    response.Headers.Append("HX-Trigger", "show-me");

    return Results.Content($"GET => {DateTime.UtcNow}");
});
```

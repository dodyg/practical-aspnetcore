# HX-Trigger Response Header with JSON payload

This example demonstrates the usage of `HX-Trigger` response header to emit custom events with JSON payload at the client([doc](https://htmx.org/headers/hx-trigger/)).

```csharp
htmx.MapGet("/", (HttpRequest request, HttpResponse response) =>
{
    response.Headers.Append("HX-Trigger", """{"show-me":{"message":"GET request"}}""");

    return Results.Content($"GET => {DateTime.UtcNow}");
});
```

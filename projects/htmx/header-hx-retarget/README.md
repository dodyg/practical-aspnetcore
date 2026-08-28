# HX-Trigger Response Header

This example demonstrates the usage of `HX-Retarget` response header to retarget an element (overriding the request) at the client using CSS selector([doc](https://htmx.org/reference/#events)).

```csharp
htmx.MapGet("/", (HttpRequest request, HttpResponse response) =>
{
    response.Headers.Append("HX-Retarget", "#get");

    return Results.Content($"GET => {DateTime.UtcNow}");
});
```

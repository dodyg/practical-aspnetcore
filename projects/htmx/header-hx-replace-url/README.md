# HX-Replace-Url Response Header

This example demonstrates the usage of `HX-Replace-Url` response header to replace the current url at the browser history ([doc](https://htmx.org/headers/hx-replace-url/)).

```csharp
htmx.MapGet("/", (HttpRequest request, HttpResponse response) =>
{
    response.Headers.Append("HX-Replace-Url", "/get");

    return Results.Content($"GET => {DateTime.UtcNow}");
});
```

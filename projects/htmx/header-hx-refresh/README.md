# HX-Refresh Response Header

This example demonstrates the usage of `HX-Refresh` response header to instruct the web browser to refresh the page ([doc](https://htmx.org/reference/#response_headers)).

```csharp
htmx.MapGet("/", (HttpRequest request, HttpResponse response) =>
{
    response.Headers.Append("HX-Refresh", "true");

    return Results.Content($"GET => {DateTime.UtcNow}");
});
```

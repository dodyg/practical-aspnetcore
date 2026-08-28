# Header HX-Reselect

This example shows how to use HTTP Response `HX-Reselect` to select which part of the response to swap using CSS selector and override `hx-select` in on the triggering element.  ([doc](https://htmx.org/reference/#response_headers))

```csharp
htmx.MapGet("/", (HttpRequest request, HttpResponse response) =>
{
    response.Headers.Append("HX-Reselect", "#get");

    return Results.Content($"""
    GET => {DateTime.UtcNow}
        <div id="get">RESELECTED GET => {DateTime.UtcNow}</div>
    """);
});
```

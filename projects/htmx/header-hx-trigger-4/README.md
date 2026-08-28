# HX-Trigger Response Header

This example demonstrates the usage of `HX-Trigger` response header to emit multiple custom events with JSON payload at the client([doc](https://htmx.org/headers/hx-trigger/)).

The `HX-Trigger` response is built manually with standard ASP.NET Core response-header APIs.

```csharp
htmx.MapGet("/", (HttpRequest request, HttpResponse response) =>
{
    var showMe = JsonSerializer.Serialize(new { message = "GET from show-me" });
    var showYou = JsonSerializer.Serialize(new { message = "GET from show-you" });

    response.Headers.Append("HX-Trigger", $$$"""{"show-me": {{{showMe}}}, "show-you": {{{showYou}}} }""");
    return Results.Content($"GET => {DateTime.UtcNow}");
});

```

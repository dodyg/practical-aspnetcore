# Automatic cross-origin (CSRF) protection

Preview 6 made `WebApplication.CreateBuilder` apps automatically reject unsafe
cross-origin requests based on the browser's `Sec-Fetch-Site` and `Origin`
headers. Preview 7 refined the middleware so it only validates endpoints whose
metadata contains `IAntiforgeryMetadata { RequiresValidation: true }` — form
binding endpoints get this automatically (Minimal API `[FromForm]`/`IFormFile`/
`IFormCollection`, Razor Pages/MVC form binding, Blazor SSR forms), so no
.NET 10 behavior is broken.

```csharp
app.MapPost("/contact", ([FromForm] ContactForm form) =>
        Results.Ok($"Thanks, {form.Name}!"));

app.MapPost("/contact-unprotected", ([FromForm] ContactForm form) => ...)
   .DisableAntiforgery(); // per-endpoint opt-out
```

Try it:

```bash
# cross-origin browser request -> 403
curl -i -X POST http://localhost:5000/contact -d "name=X" -H "Sec-Fetch-Site: cross-site"

# same-origin -> 200
curl -i -X POST http://localhost:5000/contact -d "name=Ada"
```

Controls:

- Opt an endpoint out: `.DisableAntiforgery()` (Minimal APIs) or
  `[IgnoreAntiforgeryToken]` (MVC).
- Turn it off app-wide: set the `DisableCsrfProtection` configuration key.
- Full control over the trust decision: register a custom `ICsrfProtection`
  implementation.

See the [Preview 6](https://github.com/dotnet/core/blob/main/release-notes/11.0/preview/preview6/aspnetcore.md#automatic-cross-origin-csrf-protection)
and [Preview 7](https://github.com/dotnet/core/blob/main/release-notes/11.0/preview/preview7/aspnetcore.md#breaking-changes)
release notes.

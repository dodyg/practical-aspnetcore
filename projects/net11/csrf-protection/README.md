# Automatic cross-origin (CSRF) protection

`WebApplication.CreateBuilder` apps automatically reject unsafe cross-origin requests based on the browser's `Sec-Fetch-Site` and `Origin` headers. It only validates endpoints whose metadata contains `IAntiforgeryMetadata { RequiresValidation: true }` — form
binding endpoints get this automatically (Minimal API `[FromForm]`/`IFormFile`/`IFormCollection`, Razor Pages/MVC form binding, Blazor SSR forms), so no .NET 10 behavior is broken.

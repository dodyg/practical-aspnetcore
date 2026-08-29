# TODO — ASP.NET Core 11 micro samples

This file lists micro samples to create for the new features in ASP.NET Core 11
(.NET 11, GA November 2025). It was compiled from the official release notes so
each entry can be implemented without redoing the research.

**Read before implementing anything:**

- `AGENTS.md` at the repo root — every sample must follow its conventions:
  - All application code in `Program.cs` (one concept per sample, keep it small)
  - csproj template (below)
  - `README.md` per sample, category README update, root README count + bullet
  - Verify with `dotnet watch run` inside the sample directory (no solution file)
- `projects/net11/README.md` — index to update (currently shows count `(1)`).

## Prerequisites

- SDK: the `net11/global.json` pins `11.0.100-preview.7.26381.103`
  (`rollForward: major`). Install a .NET 11 (preview or later) SDK.
- The local dev machine currently has only the .NET 10 SDK (`10.0.110`), so
  builds CANNOT be verified here — mark samples as unverified if you cannot
  build them, and note which API names still need checking against the
  installed SDK's ref assemblies.

### csproj template (use for every sample)

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net11.0</TargetFramework>
    <ImplicitUsings>true</ImplicitUsings>
    <LangVersion>preview</LangVersion>
  </PropertyGroup>
</Project>
```

Add `<PackageReference>` entries only if needed (e.g. `Microsoft.AspNetCore.OpenApi`).
Note: the existing `open-api-12` sample omits `<LangVersion>preview</LangVersion>` —
add it there too if you touch it (needed for union samples).

## Source material

Official per-preview release notes (read the linked section before implementing):

- P1: https://raw.githubusercontent.com/dotnet/core/main/release-notes/11.0/preview/preview1/aspnetcore.md
- P2: https://raw.githubusercontent.com/dotnet/core/main/release-notes/11.0/preview/preview2/aspnetcore.md
- P3: https://raw.githubusercontent.com/dotnet/core/main/release-notes/11.0/preview/preview3/aspnetcore.md
- P4: https://raw.githubusercontent.com/dotnet/core/main/release-notes/11.0/preview/preview4/aspnetcore.md
- P5: https://raw.githubusercontent.com/dotnet/core/main/release-notes/11.0/preview/preview5/aspnetcore.md
- P6: https://raw.githubusercontent.com/dotnet/core/main/release-notes/11.0/preview/preview6/aspnetcore.md
- P7: https://raw.githubusercontent.com/dotnet/core/main/release-notes/11.0/preview/preview7/aspnetcore.md

API names were taken from the preview notes; several were renamed between
previews (e.g. `InitialIndex`→`InitialItemIndex`, `GetUriWithHash`→`GetUriWithFragment`,
`EnvironmentBoundary`→`EnvironmentView`). **Double-check against the SDK you build with.**

## Placement rule

Version-scoped features go in `projects/net11/<sample-name>/` (precedent:
`open-api-12`). Features that fit an existing topic category go there instead —
see per-sample notes (signalr → `projects/signalr/`, open-telemetry → `projects/open-telemetry/`,
Blazor → `projects/blazor-ssr/`, security → `projects/security/`).

Every sample still needs: sample `README.md`, category `README.md` line, root
`README.md` bullet + count increment.

---

## Tier 1 — highest value (minimal API, single Program.cs)

### 1. `query-method` — routing an HTTP QUERY endpoint with a structured body
- **Where:** `projects/net11/query-method/`
- **What:** `open-api-12` covers QUERY in the OpenAPI document; this sample covers
  the routing side end-to-end. QUERY (draft-ietf-httpbis-safe-method-w-body) is a
  safe, idempotent method that carries a body — useful for large/structured search.
- **Demonstrate:**
  - `app.MapMethods("/search", ["QUERY"], (SearchRequest body) => ...)` — body binding
  - QUERY is treated as a *safe* method: works with `CacheOutput`, skipped by
    CSRF protection, counted in hosting metrics (see P6/P7 notes)
- **Sketch:**
  ```csharp
  var builder = WebApplication.CreateBuilder(args);
  builder.Services.AddOutputCache();
  var app = builder.Build();
  app.UseOutputCache();

  app.MapMethods("/search", ["QUERY"], (SearchRequest request) =>
      Results.Ok(SearchService.Run(request)))
     .CacheOutput();

  app.MapGet("/", () => TypedResults.Text("<form action=\"/search\" method=\"QUERY\">...</form>", "text/html"))
     .ExcludeFromDescription();

  app.Run();

  public record SearchRequest(string Query, int MaxResults, string? Category);
  ```
  Note: browsers/curl can't send QUERY bodies directly; demo via an HTML form
  with `method="QUERY"` or an HttpClient snippet in the README.
- **README:** link the IETF draft and the P4 release-notes section.

### 2. `zstandard-compression` — zstd response compression + request decompression
- **Where:** `projects/net11/zstandard-compression/`
- **What:** P3 added Zstandard to the existing response-compression and
  request-decompression middleware, enabled by default.
- **Demonstrate:**
  - `AddResponseCompression()` + `AddRequestDecompression()`
  - Configure `ZstandardCompressionProviderOptions` (Quality 1–22)
  - P4 behavior: `Vary: Accept-Encoding` is now emitted on every response when
    compression is enabled, even uncompressed ones
- **Sketch:**
  ```csharp
  builder.Services.AddResponseCompression();
  builder.Services.AddRequestDecompression();
  builder.Services.Configure<ZstandardCompressionProviderOptions>(options =>
  {
      options.CompressionOptions = new ZstandardCompressionOptions { Quality = 6 };
  });
  var app = builder.Build();
  app.UseResponseCompression();
  app.UseRequestDecompression();

  app.MapGet("/", () => string.Concat(Enumerable.Repeat("Hello, zstd! ", 1000)));
  app.MapPost("/echo", async (HttpRequest request) =>
  {
      using var reader = new StreamReader(request.Body);
      return await reader.ReadToEndAsync();
  });
  ```
- **README:** verification commands:
  - `curl -H "Accept-Encoding: zstd" -o /dev/null -w "%{content_encoding}\n" http://localhost:5000/`
  - `curl -H "Content-Encoding: zstd" --data-binary @payload.zst http://localhost:5000/echo`

### 3. `async-validation` — async validation for minimal APIs
- **Where:** `projects/net11/async-validation/`
- **What:** P5/P6. New `Microsoft.Extensions.Validation` stack with async rules:
  `AsyncValidationAttribute`, `IAsyncValidatableObject`, `AddValidation()`,
  `[ValidatableType]`. Endpoints validate before running and return 400
  `ProblemDetails` on failure.
- **Demonstrate:**
  - `builder.Services.AddValidation();` then `[ValidatableType]` on the body model
  - Custom `AsyncValidationAttribute` (async unique-email check against a service)
  - `IAsyncValidatableObject` for cross-property rules (`IAsyncEnumerable<ValidationResult>`)
  - Validators run concurrently where possible
- **Sketch:**
  ```csharp
  builder.Services.AddValidation();
  var app = builder.Build();

  app.MapPost("/register", (RegisterRequest request) => Results.Ok(request));

  app.Run();

  [ValidatableType]
  public class RegisterRequest
  {
      [Required, EmailAddress]
      public string Email { get; set; } = "";

      [Required, StringLength(100, MinimumLength = 8)]
      public string Password { get; set; } = "";
  }

  public sealed class UniqueEmailAttribute : AsyncValidationAttribute
  {
      protected override ValidationResult? IsValid(object? value, ValidationContext context) =>
          throw new InvalidOperationException("Validate this attribute with IsValidAsync.");

      protected override async Task<ValidationResult?> IsValidAsync(
          object? value, ValidationContext context, CancellationToken cancellationToken)
      {
          // simulate a remote/db lookup
          await Task.Delay(50, cancellationToken);
          return value as string == "taken@example.com"
              ? new ValidationResult("That email is already registered.")
              : ValidationResult.Success;
      }
  }
  ```
- **Verify:** POST an invalid payload → 400 problem+json with `errors`; POST a
  valid payload → 200. Watch that the async validator runs without blocking.

### 4. `unions` — C# 14 union types in minimal APIs
- **Where:** `projects/net11/unions/`
- **What:** P6/P7. `System.Text.Json` serializes C# union types natively; minimal
  APIs support union bodies, returns, `Task<Union>`, `IAsyncEnumerable<Union>`,
  `Results<T1, T2>`. OpenAPI emits `anyOf`.
- **Requirements:** `<LangVersion>preview</LangVersion>` (union syntax is preview).
- **Demonstrate:** union as return type + body; show generated OpenAPI `anyOf`.
- **Sketch:**
  ```csharp
  public record class Dog(string Name);
  public record class Cat(int Lives);
  public union Pet(Dog, Cat);

  app.MapGet("/pets/{id}", Pet (int id) => id == 0 ? new Dog("Rex") : new Cat(9));

  app.MapPost("/pets", (Pet pet) => Results.Ok(pet));
  ```
  Also show OpenAPI (`AddOpenApi()` + `MapOpenApi()`) describing the union with
  `anyOf`. Consider adding a `[JsonUnion]` classifier example if cases share a
  JSON shape (P6 note: needed to disambiguate).
- **Limits to mention in README:** JSON bodies/responses only — query/route/
  header/form binding not available; Swashbuckle/NSwag don't recognize unions yet.

### 5. `short-circuit-attribute` — `[ShortCircuit]` attribute
- **Where:** `projects/net11/short-circuit-attribute/`
- **What:** P6 (community contribution). Marks an endpoint to run immediately
  after routing, skipping the middleware pipeline (auth, CORS, logging, etc.).
  Attribute form of the existing `ShortCircuit()` convention; optional status
  code argument.
- **Demonstrate:** minimal API form + MVC controller form; show the speedup idea
  (health check / robots.txt). Use a marker middleware that sets a header to
  prove it is skipped.
- **Sketch:**
  ```csharp
  var app = WebApplication.Create(args);

  app.Use(async (context, next) =>
  {
      context.Response.Headers["X-Middleware-Ran"] = "true";
      await next(context);
  });

  app.MapGet("/health", [ShortCircuit] () => Results.Text("Healthy"));
  app.MapGet("/", () => "This endpoint ran the full pipeline.");
  ```
- **Verify:** `/health` response lacks `X-Middleware-Ran`; `/` has it.
- **README:** note `[ShortCircuit(404)]` form and that MVC controllers/actions
  can use it directly.

### 6. `csrf-protection` — automatic cross-origin (CSRF) protection
- **Where:** `projects/security/csrf-protection/` (or `net11/` if you prefer
  version-scoping)
- **What:** P6/P7. `WebApplication.CreateBuilder` apps automatically reject
  unsafe cross-origin requests based on `Sec-Fetch-Site` / `Origin`. P7
  refinement: only endpoints with `IAntiforgeryMetadata { RequiresValidation: true }`
  are validated (form-binding endpoints get this automatically) — so no
  .NET 10 behavior is broken.
- **Demonstrate:**
  - A `MapPost` form endpoint (protected automatically)
  - A curl request with `Sec-Fetch-Site: cross-site` / fake `Origin` → rejected (403)
  - Same-origin `Sec-Fetch-Site: same-origin` → allowed
  - `.DisableAntiforgery()` opt-out on a specific endpoint
- **Sketch:**
  ```csharp
  var app = WebApplication.Create(args);

  app.MapPost("/contact", (ContactForm form) => Results.Ok($"Thanks, {form.Name}!"))
     .DisableAntiforgery(); // remove this line to see protection

  app.Run();

  public record ContactForm(string Name, string Message);
  ```
  For a protected demo, bind form data (`IFormCollection` / `[FromForm]`) instead.
- **README:** explain the header-based mechanism, `DisableCsrfProtection` config
  key, and custom `ICsrfProtection` for full control.

### 7. `openapi-3.2-default-sse` — OpenAPI 3.2 by default + SSE `itemSchema`
- **Where:** `projects/net11/openapi-3.2-default-sse/`
- **What:** P6 made OpenAPI 3.2 the default generated version. P7 added
  `SseItem<T>` itemSchema for `text/event-stream` responses.
- **Demonstrate:**
  - `AddOpenApi()` + `MapOpenApi()` — check the doc is 3.2 without explicit config
  - `TypedResults.ServerSentEvents` over `IAsyncEnumerable<SseItem<Todo>>` → the
    generated doc describes the per-event payload via `itemSchema`
- **Sketch:**
  ```csharp
  var builder = WebApplication.CreateBuilder(args);
  builder.Services.AddOpenApi();
  var app = builder.Build();
  app.MapOpenApi();

  app.MapGet("/todos/stream", (CancellationToken ct) =>
      TypedResults.ServerSentEvents(GetTodosAsync(ct)))
     .WithName("StreamTodos");

  app.Run();

  static async IAsyncEnumerable<SseItem<Todo>> GetTodosAsync(
      [EnumeratorCancellation] CancellationToken ct = default)
  {
      foreach (var todo in Todos.All)
      {
          yield return new SseItem<Todo>(todo) { EventId = todo.Id.ToString() };
          await Task.Delay(1000, ct);
      }
  }

  public record Todo(int Id, string Title, bool Done);
  public static class Todos
  {
      public static readonly List<Todo> All =
          [new(1, "Learn ASP.NET Core 11", false), new(2, "Write a micro sample", true)];
  }
  ```
- **Verify:** fetch `/openapi/v1.json`, confirm `openapi: 3.2.0` and the SSE
  response has `itemSchema` referencing `#/components/schemas/Todo`.
- **README:** note that returning `IAsyncEnumerable<SseItem<T>>` directly would
  serialize as JSON instead of SSE — must go through `TypedResults.ServerSentEvents`.

---

## Tier 2 — strong candidates (still micro)

### 8. `openapi-file-results` — binary file responses in OpenAPI
- **Where:** `projects/net11/openapi-file-results/`
- **What:** P1/P4. `FileContentResult`, `FileContentHttpResult`, `FileStreamResult`,
  `FileStreamHttpResult` now map to `type: string, format: binary` in generated
  docs via `Produces<T>` / `ProducesResponseType<T>`.
- **Sketch:**
  ```csharp
  builder.Services.AddOpenApi();
  app.MapOpenApi();
  app.MapPost("/file", () => TypedResults.File("Hello"u8.ToArray()))
     .Produces<FileContentHttpResult>(contentType: MediaTypeNames.Application.Octet);
  ```

### 9. `openapi-multiple-produces` — multiple `Produces<T>` per status code
- **Where:** `projects/net11/openapi-multiple-produces/`
- **What:** P5. Multiple response types for the same status code are no longer
  collapsed; each media type gets its own content entry (or `anyOf` when types
  share a content type). Same for MVC `[ProducesResponseType]`.
- **Sketch:**
  ```csharp
  app.MapGet("/ping", () => Results.Text("pong"))
     .Produces<string>(contentType: "text/plain")
     .Produces<PingResult>(contentType: "application/json");

  public record PingResult(string Message, DateTime At);
  ```

### 10. `endpoint-filter-binding-failures` — filters observe parameter-binding failures
- **Where:** `projects/net11/endpoint-filter-binding-failures/`
- **What:** P4. With filters present, the filter pipeline runs even when binding
  fails; filter can read `HttpContext.Response.StatusCode == 400` and write its
  own response. Set `RouteHandlerOptions.ThrowOnBadRequest = false` in
  Development so the filter sees a 400 instead of an exception page.
- **Sketch:**
  ```csharp
  builder.Services.Configure<RouteHandlerOptions>(o => o.ThrowOnBadRequest = false);
  var app = builder.Build();

  app.MapPost("/items", (Item item) => Results.Ok(item))
     .AddEndpointFilter(async (context, next) =>
     {
         var result = await next(context);
         if (context.HttpContext.Response.StatusCode == 400)
             return Results.Problem("Invalid payload — custom message from the filter.",
                 statusCode: 400);
         return result;
     });

  app.Run();
  public record Item(string Name, int Quantity);
  ```

### 11. `output-cache-policy-provider` — custom `IOutputCachePolicyProvider`
- **Where:** `projects/output-cache-middleware/` or `projects/net11/`
- **What:** P1 (community contribution). Interface for dynamic output cache
  policy selection (load policies from config/DB, tenant-specific rules).
  `GetBasePolicies()` and `GetPolicyAsync(policyName)`.
- **Sketch:** register `AddOutputCache()`, implement
  `IOutputCachePolicyProvider` returning a policy that varies on a tenant header,
  then verify two requests with different tenant headers produce distinct cached
  responses.

### 12. `signalr-auth-refresh` — SignalR authentication refresh
- **Where:** `projects/signalr/` (new sample in that category)
- **What:** P6. Server exposes a `/refresh` endpoint alongside `/negotiate`;
  `.NET` client re-authenticates before token expiry without dropping the
  connection. (JS client + Azure SignalR Service not yet supported in preview.)
- **Demonstrate:** server side only — `EnableAuthenticationRefresh = true` on
  `MapHub` options, optional `OnAuthenticationRefresh` predicate, and
  `OnAuthenticationRefreshedAsync` override in the hub. Optionally a .NET client
  with `WithAuthenticationRefresh` in the same Program.cs.
- **Sketch:**
  ```csharp
  app.MapHub<ChatHub>("/chat", options =>
  {
      options.EnableAuthenticationRefresh = true;
      options.OnAuthenticationRefresh = context => ValueTask.FromResult(true);
  });

  public class ChatHub : Hub
  {
      public override Task OnAuthenticationRefreshedAsync()
      {
          // connection.User has been updated with the refreshed token
          return Task.CompletedTask;
      }
  }
  ```

### 13. `signalr-client-cancellation` — cancel hub invocations from the client
- **Where:** `projects/signalr/`
- **What:** P6. Client can cancel a regular (non-streaming) invocation by
  canceling the `CancellationToken` passed to `InvokeAsync`; server hub method's
  `CancellationToken` parameter is triggered.
- **Sketch (hub):**
  ```csharp
  public class WorkHub : Hub
  {
      public async Task LongRunningWork(CancellationToken cancellationToken)
      {
          await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
      }
  }
  ```
  Client: `var work = connection.InvokeAsync("LongRunningWork", cts.Token); cts.Cancel();`

### 14. `native-otel-tracing` — OpenTelemetry semantic conventions built in
- **Where:** `projects/open-telemetry/`
- **What:** P2. ASP.NET Core populates OTel semantic convention attributes
  (`http.request.method`, `url.path`, `http.response.status_code`,
  `server.address`, ...) on the `Microsoft.AspNetCore` activity source natively —
  no `OpenTelemetry.Instrumentation.AspNetCore` package needed.
- **Sketch:** `AddOpenTelemetry().WithTracing(t => t.AddSource("Microsoft.AspNetCore").AddConsoleExporter())`.
  Mention the opt-out AppContext switch
  `Microsoft.AspNetCore.Hosting.SuppressActivityOpenTelemetryData`.
- **README:** contrast with the old approach (instrumentation package) and link
  the OTel HTTP server span spec.

### 15. `validation-localization` — localized validation messages
- **Where:** `projects/net11/validation-localization/`
- **What:** P5/P7. `AddValidation()` + `AddLocalization()` now localize messages
  and display names automatically (P7: no separate package; keys are emitted by
  the validation source generator into the assembly). Custom
  `IStringLocalizerFactory` supported; `ValidationOptions.LocalizerProvider` for
  shared resource files.
- **Note:** RESX files are separate assets (repo prefers single-file samples) —
  either ship a tiny `.resx` (acceptable; there's precedent for multi-file
  samples where truly needed) or demo with an inline custom
  `IStringLocalizerFactory` in Program.cs.
- **Sketch:**
  ```csharp
  builder.Services.AddLocalization();
  builder.Services.AddValidation();
  ```
  ```csharp
  [ValidatableType]
  public class CustomerModel
  {
      [Display(Name = "CustomerName")]          // resource key for the display name
      [Required(ErrorMessage = "NameRequired")] // resource key for the message
      public string? Name { get; set; }
  }
  ```

### 16. `tls-channel-binding` — channel-binding token from `ITlsConnectionFeature`
- **Where:** `projects/security/`
- **What:** P7. `ITlsConnectionFeature.TryGetChannelBindingBytes(ChannelBindingKind.Endpoint, out ReadOnlyMemory<byte> cbt)`
  exposes the TLS channel-binding token to defend against relay attacks.
- **Requirements:** app must use HTTPS (dev cert). Kestrel returns the binding
  from `SslStream.TransportContext.GetChannelBinding`; IIS/HTTP.sys have their
  own story (`HttpSysOptions.HttpAuthenticationHardeningLevel`).
- **Sketch:** a middleware that reads the feature and logs/hashes the CBT.
- **README:** explain channel binding + the HTTP.sys hardening levels
  (Legacy/Medium/Strict).

---

## Tier 3 — Blazor samples (Razor components; put in `projects/blazor-ssr/`)

These need Razor components/`MapRazorComponents`, so they don't fit the single-
`Program.cs` rule — that's expected and fine (precedent: Razor Pages/Blazor
samples in this repo are multi-file).

- **`CacheView`** (P7) — cache an SSR subtree; `ExpiresAfter`/`ExpiresSliding`/
  `ExpiresOn`, `VaryByRoute`/`VaryByQuery`/`VaryByCulture`/`VaryByUser`, and the
  `[CacheBehavior]`/`[CacheCondition]` attributes (built-ins: `AntiforgeryToken`
  Rerender, `AuthorizeView` Throw+User, `QuickGrid` Throw+Query, `Virtualize` Throw).
  Also show `AddHybridCache()` integration. One of the biggest P7 features.
- **`Label` + `DisplayName` components** (P1) — accessible labels (nested and
  `for`/`id` patterns) and display names from `[Display]`/`[DisplayName]`.
- **`[SupplyParameterFromTempData]`** (P4) — POST-redirect-GET status messages;
  paired with `ITempData` cascading parameter.
- **`[SupplyParameterFromSession]`** (P5) — session-backed component state
  (e.g. checkout step), requires `AddSession()` + `UseSession()`.
- **`QuickGrid` in static SSR** (P5) — sorting/pagination via URL query state
  without interactivity; `OnRowClick` (P1); `InitialItemIndex`/`ScrollToItemAsync`
  (P7, note the P6→P7 rename).
- **`EnvironmentView`** (P1, renamed in P6 — do NOT use the old
  `EnvironmentBoundary` name) — `Include`/`Exclude` environment-based rendering.
- **SSR client-side form validation** (P5/P7) — `DataAnnotationsValidator` emits
  client-side rules for static SSR forms; new `valid`/`invalid`/`modified` CSS
  classes; `EditContext.ValidateAsync` (sync `Validate` is now obsolete).
- **Circuit pause** (P4/P7) — `Circuit.RequestCircuitPauseAsync()` via
  `CircuitHandler.OnConnectionUpAsync`; opt-in `Microsoft.AspNetCore.Components.Server.AutoPause`
  package with `AddAutoPause` on `BrowserOptions` (hidden-tab auto-pause).

---

## Not code samples (document instead)

- `dotnet new mcpserver` now ships with the SDK (P4) — template, not sample code.
- `dotnet new webworker` / Blazor Web Worker template + standalone-WASM Gateway
  (`Microsoft.AspNetCore.Components.Gateway`, P4/P5/P7) — dev tooling/templates.
- `dotnet user-jwts create --file app.cs` (P6) — tooling change for file-based apps.
- Breaking changes worth a README note (P4/P7): `%2F` preserved in HTTP/1.1
  absolute-form targets; MVC `CompatibilityVersion` removed; `EditContext.Validate`
  obsolete; `WebApplicationFactory.ConfigureWebApplicationBuilder` rename;
  `UseWebAssemblyDebugging` obsolete / DevServer package deprecated.

---

## Per-sample delivery checklist (from AGENTS.md)

- [ ] Create `projects/<category>/<sample-name>/` (kebab-case)
- [ ] Add `<sample-name>.csproj` (template above; `net11.0`)
- [ ] ALL application code in `Program.cs`
- [ ] Add `README.md` (one-sentence description, useful snippets, doc links)
- [ ] Update category `README.md` (e.g. `projects/net11/README.md` — currently `(1)`)
- [ ] Increment count + add bullet in root `README.md`
- [ ] Verify with `dotnet watch run` in the sample directory (needs .NET 11 SDK)
- [ ] If the machine only has .NET 10 SDK: say so in the PR/commit and leave
      verification to a machine with the .NET 11 SDK

# ASP.NET Core 11 (18)

These samples require SDK [11.0.100-preview.7](https://dotnet.microsoft.com/en-us/download/dotnet/11.0)

- [open-api-12](open-api-12)

  This sample demonstrates support for the new [HTTP Query method](https://httpwg.org/http-extensions/draft-ietf-httpbis-safe-method-w-body.html).

- [query-method](query-method)

  This sample demonstrates routing an HTTP QUERY endpoint with a structured JSON body, including output caching (QUERY is a safe method).

- [zstandard-compression](zstandard-compression)

  This sample demonstrates Zstandard (zstd) response compression and request decompression, enabled by default in .NET 11.

- [async-validation](async-validation)

  This sample demonstrates the new `Microsoft.Extensions.Validation` stack: `AddValidation()`, `[ValidatableType]`, `AsyncValidationAttribute` and `IAsyncValidatableObject`.

- [validation-localization](validation-localization)

  This sample demonstrates localized validation messages and display names using `AddValidation()` and `AddLocalization()`.

- [short-circuit-attribute](short-circuit-attribute)

  This sample demonstrates the `[ShortCircuit]` attribute, which runs an endpoint immediately after routing, skipping the middleware pipeline.

- [open-api-13](open-api-13)

  This sample demonstrates OpenAPI 3.2 as the default document version and the `itemSchema` description of Server-Sent Events responses.

- [open-api-14](open-api-14)

  This sample demonstrates binary file responses (`FileContentHttpResult`) described as `type: string, format: binary` in OpenAPI documents.

- [open-api-15](open-api-15)

  This sample demonstrates multiple `Produces<T>()` per status code producing separate content entries in the OpenAPI document.

- [endpoint-filter-binding-failures](endpoint-filter-binding-failures)

  This sample demonstrates endpoint filters observing parameter-binding failures and writing their own 400 response.

- [native-otel-tracing](native-otel-tracing)

  ASP.NET Core 11 populates OpenTelemetry semantic convention attributes on the `Microsoft.AspNetCore` activity source natively — no instrumentation package needed.

- [unions](unions)

  This sample demonstrates C# union types used as minimal API return types and JSON request bodies.

## Blazor SSR

- [cache-view](cache-view)

  Caches the rendered output of an SSR subtree (`ExpiresAfter`, vary-by dimensions) and shows the `[CacheBehavior]` violation error for uncacheable components like `Virtualize`.

- [environment-view](environment-view)

  `EnvironmentView` renders content based on the hosting environment (`Include`/`Exclude`), like the MVC environment tag helper.

- [label-display-name](label-display-name)

  Accessible `Label` (nested and for/id patterns) and `DisplayName` components that read from `[Display]`/`[DisplayName]` attributes.

- [quick-grid-ssr](quick-grid-ssr)

  `QuickGrid` sorting and pagination work without interactivity — sortable headers and paginator render as URL query-state links.

- [supply-parameter-from-session](supply-parameter-from-session)

  `[SupplyParameterFromSession]` reads and writes HTTP session values directly on SSR component properties (e.g. checkout progress).

- [supply-parameter-from-temp-data](supply-parameter-from-temp-data)

  `[SupplyParameterFromTempData]` reads and writes TempData values directly on SSR component properties (POST-redirect-GET status messages).

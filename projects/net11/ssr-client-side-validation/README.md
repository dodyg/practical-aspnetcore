# SSR client-side form validation

Blazor SSR forms validates in the browser without a server round-trip: the server renders the metadata for the `DataAnnotationsValidator`
rules, and the Blazor JS code enforces them client-side. It's enabled by default for SSR forms that include a `DataAnnotationsValidator`; both enhanced and non-enhanced forms are supported.

Look for `blazor-client-validation-data` tag on the rendered page. 

Blazor SSR adds `valid` / `invalid` / `modified` CSS classes on inputs, and `EditContext.ValidateAsync()` replaces the now-obsolete synchronous `Validate()`.


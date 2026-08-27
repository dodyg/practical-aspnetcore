# Blazor SSR - [SupplyParameterFromSession]

Blazor now has `[SupplyParameterFromSession]`. It reads and writes HTTP session values directly on Blazor SSR component properties, in the same declarative style as `[SupplyParameterFromQuery]` and `[SupplyParameterFromForm]`. Values are serialized with `System.Text.Json`.

Click "Next step": the value survives subsequent requests (check with a new tab — session cookies make the state follow the browser).

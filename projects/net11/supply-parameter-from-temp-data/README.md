# Blazor SSR [SupplyParameterFromTempData]

Blazor SSR now adds `[SupplyParameterFromTempData]`. It reads and writes TempData values directly on a Blazor SSR component property, in the same style as `[SupplyParameterFromQuery]` and `[SupplyParameterFromForm]`.

Setting the property writes through to TempData, so the value survives a redirect-after-post (e.g. "Your changes were saved."). If the property name doesn't match the TempData key, set `Name` on the attribute.

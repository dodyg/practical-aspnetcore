# SSR client-side form validation

Preview 5 makes Blazor SSR forms validate in the browser without a server
round-trip: the server renders the metadata for the `DataAnnotationsValidator`
rules, and the Blazor JS code enforces them client-side. It's enabled by
default for SSR forms that include a `DataAnnotationsValidator`; both enhanced
and non-enhanced forms are supported.

```razor
<EditForm Model="Model" Enhance FormName="registration" OnValidSubmit="HandleValidSubmit">
    <DataAnnotationsValidator />
    <InputText @bind-Value="Model!.Email" id="Email" />
    <ValidationMessage For="@(() => Model!.Email)" />
    ...
</EditForm>
```

Preview 7 adds `valid` / `invalid` / `modified` CSS classes on inputs, and
`EditContext.ValidateAsync()` replaces the now-obsolete synchronous
`Validate()`.

See the [Preview 5](https://github.com/dotnet/core/blob/main/release-notes/11.0/preview/preview5/aspnetcore.md#blazor-ssr-supports-client-side-validation)
and [Preview 7](https://github.com/dotnet/core/blob/main/release-notes/11.0/preview/preview7/aspnetcore.md#blazor-ssr-client-side-form-validation-improvements)
release notes.

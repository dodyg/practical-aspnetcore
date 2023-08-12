# Form Handling in Blazor SSR - Automatic binding using EditForm and [SupplyParameterFromForm]

This example shows how to perform automatic data binding for a form `POST` request using `<EditForm/>` and `[SupplyParameterFromForm]`. `EditForm` will generate the antiforgery token so there is no need to include `<AntiforgeryToken/>` component manually.
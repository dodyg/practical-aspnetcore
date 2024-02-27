# Form Handling in Blazor SSR - Automatic binding using [SupplyParameterFromForm] 

This example shows how to perform automatic data binding for a form `POST` request using `[SupplyParameterFromForm]`. We will use normal `<form>` tag in this case. Never forget to include `<AntiforgeryToken />` in your form otherwise your POST request won't be processed. 
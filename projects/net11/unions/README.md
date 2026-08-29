# C# union types in minimal APIs

This sample demonstrates [C# union types](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/union)
used as minimal API return types and JSON request bodies. `System.Text.Json` serializes unions natively and the built-in OpenAPI generator describes them with `anyOf` schemas.


Check the generated document at `/openapi/v1.json` — each union case is listed under `anyOf` referencing the standalone component schema.

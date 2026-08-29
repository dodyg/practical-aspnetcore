# C# union types in minimal APIs

This sample demonstrates [C# union types](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/union)
(a preview feature in .NET 11) used as minimal API return types and JSON request
bodies. `System.Text.Json` serializes unions natively and the built-in OpenAPI
generator describes them with `anyOf` schemas.

```csharp
public record class Dog(string Name);
public record class Cat(int Lives);
public union Pet(Dog, Cat);

app.MapGet("/pets/{id}", Pet (int id) => id == 0 ? new Dog("Rex") : new Cat(9));
app.MapPost("/pets", (Pet pet) => Results.Ok(pet));
```

Requirements and limits:

- `<LangVersion>preview</LangVersion>` — union syntax is a preview language feature.
- Only JSON bodies and responses are supported. Binding a union from the query
  string, route values, headers, or form fields is not available yet.
- When multiple cases serialize to the same JSON shape, disambiguate them with a
  `[JsonUnion]` classifier.
- Third-party generators (Swashbuckle, NSwag) do not recognize unions yet.

Check the generated document at `/openapi/v1.json` — each union case is listed
under `anyOf` referencing the standalone component schema.

See the [Preview 6 release notes](https://github.com/dotnet/core/blob/main/release-notes/11.0/preview/preview6/aspnetcore.md#unions-in-aspnet-core).

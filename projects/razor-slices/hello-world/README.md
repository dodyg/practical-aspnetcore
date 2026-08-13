# Hello world

This is a hello world sample based on [RazorSlices](https://github.com/DamianEdwards/RazorSlices), a Razor-based template engine. The Nuget Package is available [here](https://www.nuget.org/packages/RazorSlices).

Slices inherit from `RazorSlice` (or `RazorSlice<TModel>` for slices with a model) and are returned from a minimal API via the source-generated, strongly-typed `Create` method:

```csharp
app.MapGet("/", () => HelloWorld.Slices.Index.Create("Hello world"));
```

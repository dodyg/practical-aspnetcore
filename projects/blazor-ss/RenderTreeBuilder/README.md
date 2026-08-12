# RenderTreeBuilder

`RenderTreeBuilder` shows a Blazor (server) component implemented without Razor syntax, inheriting from the abstract class `ComponentBase` and implementing the `BuildRenderTree` method.

The component is rendered into a minimal API response using the public `IComponentPrerenderer` service. The prerendered HTML must be written inside the renderer's `Dispatcher` (via `Dispatcher.InvokeAsync`), otherwise an `InvalidOperationException` is thrown. In .NET 10 the interactive render mode is `RenderMode.InteractiveServer` (the old `RenderMode.Server` was removed).

Contribution by [ericsink](https://github.com/ericsink).


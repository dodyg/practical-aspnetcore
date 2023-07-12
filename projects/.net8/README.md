# ASP.NET 8.0 Preview 7 (17)

These samples require [.NET 8.0 Preview 7](https://github.com/dotnet/installer#table). 

* [QuickGrid One](QuickGridOne)
  
  This sample demonstrates a simple usage of QuickGrid component displaying int, string, date, and boolean data types.

* [RazorComponentOne](RazorComponentOne)

  This sample demonstrates a simple usage of Razor Component component.

* [RazorComponentTwo](RazorComponentTwo)

  This sample demonstrates rendering a Razor Component from Minimal API via  `RazorComponentResult` and passing data via a dictionary.

* [RazorComponentThree](RazorComponentThree)

  This sample demonstrates rendering a Razor Component from Minimal API via `RazorComponentResult` and passing data via anonymous object.

* [RazorComponentFour](RazorComponentFour)

  This sample demonstrates rendering a Razor Component using `Html.RenderComponentAsync` and passing data via anonymous object.

* [RazorComponentFive](RazorComponentFive)

  This sample demonstrates rendering a Razor Component from a MVC Controller via `RazorComponentResult` and passing data via a dictionary.

* [RazorComponentSix](RazorComponentSix)

  This sample demonstrates the new section functionality using `SectionOutlet` to mark a section and `SectionContent` to supply the content to a section.

* [RazorComponentSeven](RazorComponentSeven)

  This sample demonstrates a Razor Component Page SSR (Server Side Render) hosting Razor Component with Blazor Server Side (interactive) and Blazor Streaming Rendering.

* [RazorComponentEight](RazorComponentEight)

  This sample demonstrates a Razor Component Page SSR (Server Side Render) hosting Razor Component with Blazor Web Assembly (interactive) and Blazor Streaming Rendering.

* [RazorComponentNine](RazorComponentNine)

  This sample demostrates the new attribute `[SupplyParameterFromQuery]` that allows Blazor component to get values directly from query string.

* [Request Timeout](request-timeout)

  This sample demonstrates how to configure a request timeout.

* [Request Timeout Policy](request-timeout-2)

  Trigger exception on a timeout using `HttpContext.RequestAborted.ThrowIfCancellationRequested()` on a timeout that was specified using a named policy.

* [Request Timeout Policy](request-timeout-3)

  Trigger exception on a timeout using `HttpContext.RequestAborted.ThrowIfCancellationRequested()` on a default timeout policy.

* [Short Circuit](map-short-circuit)

  Use `MapShortCircuit` or `.ShortCircuit()` to efficiently respond to a request without going through a middleware pipeline run. 

* [SlimBuilder](slim-builder)

  `WebApplication.CreateSlimBuilder` creates `WebApplicationBuilder` with minimal configuration defaults.

## Blazor SSR Form Handling

 * [RazorFormHandling](RazorFormHandling)
   
   This example shows how to perfom manual data binding for a form `POST` request using `FormDataProvider`.
  
 * [RazorFormHandlingTwo](RazorFormHandlingTwo)

   This example shows how to perform automatic data binding for a form `POST` request using `[SupplyParameterFromForm]`.
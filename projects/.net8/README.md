# ASP.NET 8.0 Preview 7 (39)

These samples require [.NET 8.0 RC 1](https://github.com/dotnet/installer#table). 

* [QuickGrid One](QuickGridOne)
  
  This sample demonstrates a simple usage of QuickGrid component displaying int, string, date, and boolean data types.

* [RazorComponentOne](RazorComponentOne)

  This sample demonstrates a simple usage of Razor Component component in SSR (Server Side Rendering).

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

* [RazorComponentTen](RazorComponentTen)

  This sample demonstrates a Razor Component Page SSR that handle a POST form, hosts "number" component with with Streaming rendering and hosts "interactive" components backed by Blazor Web Assembly and Blazor Server.

* [RazorComponentElevent](RazorComponentEleven)

  This sample shows how to update the UI multiple times using `StateHasChanged();` while in streaming rendering mode.

* [Short Circuit](map-short-circuit)

  Use `MapShortCircuit` or `.ShortCircuit()` to efficiently respond to a request without going through a middleware pipeline run. 

* [SlimBuilder](slim-builder)

  `WebApplication.CreateSlimBuilder` creates `WebApplicationBuilder` with minimal configuration defaults.

* [EmptyBuilder](empty-builder)

  `WebApplication.CreateEmptyBuilder` creates `WebApplicationBuilder` with no built-in behavior.

## Blazor SSR Form Handling

 * [RazorFormHandlingOne](RazorFormHandlingOne)
   
   This example shows how to perform automatic data binding for a form `POST` request using `[SupplyParameterFromForm]`. We will use normal `<form>` tag in this case.

 * [RazorFormHandlingTwo](RazorFormHandlingTwo)

   This example shows how to perform automatic data binding for a form `POST` request using `<EditForm/>` and `[SupplyParameterFromForm]`.

 * [RazorFormHandlingThree](RazorFormHandlingThree)

   This example shows how to perform **multiple** automatic data binding for a form `POST` request using `[SupplyParameterFromForm]`. We will use normal `<form>` tag in this case.

 * [RazorFormHandlingFour](RazorFormHandlingFour)

   This example shows how to perform **multiple** automatic data binding for a form `POST` request using `<EditForm/>` and `[SupplyParameterFromForm]`. 

 * [RazorFormHandlingFive](RazorFormHandlingFive)

   This example shows how to perform data validation using `DataAnnotationsValidator` and `EditForm`.

## Mix and Match

  * [RazorMixMatchOne](RazorMixMatchOne)

    This example shows how to use Blazor SSR with MVC in the same system.
  
  * [RazorMixMatchTwo](RazorMixMatchTwo)

    This example shows how to use Blazor SSR with Razor Pages in the same system.

  * [RazorMixMatchThree](RazorMixMatchThree)

    This example shows how to use Blazor SSR with Minimal API in the same system.

  * [RazorMixMatchFour](RazorMixMatchFour)

    This example shows how to use Blazor SSR with MVC, Razor Pages and Minimal API in the same system.

## Component

  * [ComponentTwentyThree](ComponentTwentyThree)

    This sample shows how to set root level cascading values without using `<CascadingValue/>` component. 
  
  * [ComponentTwentyFour](ComponentTwentyFour)

    This sample shows how to set root level **named** cascading values without using `<CascadingValue/>` component. 

  * [ComponentTwentyFive](ComponentTwentyFive)

    This sample shows how to set root level **dynamic** cascading values using `CascadingValueSource`.

  * [ComponentTwentySix](ComponentTwentySix)

    This sample shows how to set root level **named dynamic** cascading values using `CascadingValueSource`.

## Minimal API

  * [Minimal API Form Model Binding](minimal-api-form-model-binding)

    This sample demonstrates the ability to use `[FromForm]` binding in Minimal API.


## Request Timeout

  * [Request Timeout](request-timeout)

    This sample demonstrates how to configure a request timeout in Minimal API.

  * [Request Timeout Policy](request-timeout-2)

    Trigger exception on a timeout using `HttpContext.RequestAborted.ThrowIfCancellationRequested()` on a timeout that was specified using a named policy in Minimal API.

  * [Request Timeout Policy](request-timeout-3)

    Trigger exception on a timeout using `HttpContext.RequestAborted.ThrowIfCancellationRequested()` on a default timeout policy in Minimal API.

  * [Request Timeout](request-timeout-4)

    Use `RequestTimeout` attribute in an MVC controller to specify when timeout is trigerred. 

  * [Request Timeout](request-timeout-5)

    Use `RequestTimeout` attribute in an MVC controller to use a named policy. 
    
  * [Request Timeout](request-timeout-6)

    Use default timeout policy in an MVC controller. 

## IExceptionHandler

  * [IExceptionHandler](iexception-handler)
  
    Implement `IExceptionHandler` to handle ASP.NET Core exceptions.

  * [Multiple IExceptionHandler](iexception-handler-2)
  
    Implement multiple `IExceptionHandler` to handle ASP.NET Core exceptions.

## Keyed Services

  * [Keyed Services in Minimal API](keyed-service)

    Use `IServiceProvider.GetRequiredKeyedService<T>(key)` to get different type of implementation instances based on string key in Minimal API

  * [Keyed Services in MVC](keyed-service-2)

    Use `IServiceProvider.GetRequiredKeyedService<T>(key)` to get different type of implementation instances based on string key in MVC
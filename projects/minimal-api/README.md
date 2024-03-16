# Minimal API (39)

* [Hello World](hello-world)
  
  This is Hello World ASP.NET Core 6 app using the new `WebApplication` instead of `IHostBuilder`.

## Minimal Routing (9)

In these examples we are using the familiar `Host.CreateDefaultBuilder(args)` host configuration style to emphasis that the new Minimal Routing feature works in both existing style and also in the new minimal hosting API.

* [Map](map)

  This is a brand new feature that allows the creation of Web API without using Controllers.
  
* [Accessing HttpContext in Map](map-2)

  This sample shows how to access HttpContext in `Map`.

* [Accessing Objects in Map](map-3)

  This sample shows how to access objects `Map`.

* [Setup Open API support](map-4)

  This sample shows how to setup Open API support with Swashbuckle.

* [HttpRequest injection](map-5)

  This sample shows how to use `HttpRequest` directly without relying on `HttpContext`.

* [IEndpointRouteBuilder](map-6)

  This sample shows the use of `IEndPointRouteBuilder` to organize code.

* [MapPost with JSON model binder](map-post)

  This sample shows how JSON model binder works in `MapPost`.

* [MapPost with JSON model binder - 2](map-post-2)

  This sample shows how JSON model binder works in `MapPost` using static method.

* [MapMethods with JSON model binder](map-methods)

  This sample shows how to handle multiple HTTP Verbs request using `MapMethods`. In this example we send 'PUT', 'POST' and 'PATCH' requests.

## Route Constraints (12)

  * [Route Constraints - int, min, max and range](route-constraints-int)
  
    This example shows route constraint for integer, the minimum and maximum value, and the range.

  * [Route Constraints - decimal, float and double](route-constraints-decimal)
    
    This example shows route constraint for decimal, float and double.

## Link Generator (1)

 * [Link Generator - Generate path from a route name](link-generator-path-by-route-name)
    
   This sample shows how to generate a url path from a route name.

## Parameter Binding (10)

 * [Route binding - implicit](parameter-binding-route-implicit)

   This example shows usage of implicit route parameter binding of `int` and `string` types.

 * [Route binding - explicit](parameter-binding-route-explicit)

   This example shows usage of explicit route parameter binding of `int` and `string` types using `[FromRoute]` attribute.

 * [Query string binding - implicit](parameter-binding-query-string-implicit)

   This example shows usage of implicit query string parameter binding of `int` and `string` types.

 * [Query string binding - explicit](parameter-binding-query-string-explicit)

   This example shows usage of explicit query string parameter binding of `int` and `string` types using '[FromQuery]' attribute.

 * [Special types binding](parameter-binding-special-types)

   This example shows usage of special types binding e.g. - `HttpContext`, `HttpRequest`, `HttpResponse`, `CancellationToken`, and `ClaimsPrincipal`.

 * [Header binding - explicit](parameter-binding-header-explicit)

   This example shows usage of explicit header parameter binding of `string` type using '[FromHeader]' attribute.

 * [Json binding - implicit](parameter-binding-json-implicit)

   This example shows usage of explicit json parameter.

 * [Json binding - explicit](parameter-binding-json-explicit)

   This example shows usage of explicit json parameter binding using '[FromBody]' attribute.

 * [Custom binding to types over Route, Query or Header](parameter-binding-custom-try-parse)

   This example shows usage of custom binding to types over Route, Query or Header via implementation of static `TryParse` method.

*  [Custom binding - full control](parameter-binding-custom-bind-async)

   This shows how to take full control of the binding process by implementing `BindAsync` static method in your type.

## Antiforgery

* [Antiforgery Token on HTML form](anti-forgery-1)
  
  This shows how to use the IAntiforgery interface to generate a token for use in an HTML form.

* [Antiforgery Token on AJAX call](anti-forgery-2)
  
  This shows how to use the IAntiforgery interface to generate a token for use in AJAX call.

* [Cross site antiforgery](anti-forgery-3)
  
  This shows how to implement cross site antiforgery (e.g. the API is located in a different domain).

## Endpoint Filter

* [Endpoint Filter - 1](endpoint-filter-1)

  This sample shows how to apply an Endpoint filter to your minimal API.

* [Endpoint Filter - 2](endpoint-filter-2)

  This sample shows how to apply multiple Endpoint filters to your minimal APIs.

* [Endpoint Filter - 3](endpoint-filter-3)

  This examples shows the sequence of code execution before `RouteHandlerFilterDelegate ` and after `RouteHandlerFilterDelegate` in multiple endpoint filters.

* [Endpoint Filter - 4](endpoint-filter-4)

  Use `IStatusCodeHttpResult` to detect return result in filter.


## Route Group

* [Route Group - 1](map-group-1)

  `MapGroup()` extension methods allow grouping of endpoints with a common prefix. It also allow group metadata to be attached to the group.

* [Route Group - 2](map-group-2)

  Use `.WithTags()`, `.WithDescription()`, `.WithSummary()` to enrich OpenAPI information for all the endpoints in the group.

* [Route Group - 3](map-group-3)

  Use `.ExcludeFromDescription` to exclude endpoints from OpenAPI description.


## Minimal API Improvements

  * [Minimal API Form Model Binding](minimal-api-form-model-binding)

    This sample demonstrates the ability to use `[FromForm]` binding in Minimal API.

* [Typed Results - 1](typed-results-1)
  
  `Microsoft.AspNetCore.Http.TypedResults` static class is the “typed” equivalent of the existing `Microsoft.AspNetCore.Http.Results` class.

* [WithOpenApi - 1](open-api-1)

  This sample demonstrate the usage of `WithOpenApi` extension method available in  `Microsoft.AspNetCore.OpenApi` to customize OpenAPI operation information.

* [Results<> Union Type](open-api-2)

  `Results<TResult1, TResult2, TResultN>` provides better description of the result of the operation that OpenAPI/Swagger can use in describing the API.

* [IFormFile](iform-file)
  
  This sample demonstrates the usage of `IFormFile` to upload a file in Minimal API.

* [IFormFileCollection](iform-file-collection)
  
  This sample demonstrates the usage of `IFormFileCollection` to upload multiple files in Minimal API.

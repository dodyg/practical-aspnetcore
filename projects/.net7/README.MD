# .NET 7 (40)

Samples in this section require .NET 7. You can download it from [here](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).

* [Infer dependency from action parameter](mvc-infer-dependency-from-action)

  There is no need for `[FromServices]` attribute anymore to inject a dependency to your action method.

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


## Route Group

* [Route Group - 1](map-group-1)

  `MapGroup()` extension methods allow grouping of endpoints with a common prefix. It also allow group metadata to be attached to the group.

* [Route Group - 2](map-group-2)

  Use `.WithTags()`, `.WithDescription()`, `.WithSummary()` to enrich OpenAPI information for all the endpoints in the group.

* [Route Group - 3](map-group-3)

  Use `.ExcludeFromDescription` to exclude endpoints from OpenAPI description.

## Authentication

* [Authentication - JWT](authentication-1)

  This sample shows the usage of the simplified authentication and authorization via `builder.AddAuthentication().AddJwtBearer();`.

* [Authentication - Cookie](authentication-2)

  This sample shows the usage of the simplified authentication and authorization via `builder.AddAuthentication().AddCookie();`.

* [Authentication - Mixed](authentication-3)

  This sample shows how to use both JWT and Cookie authentications in the same application.


## Problem Details

* [Problem Details - 1](problem-details)

  This sample shows how to enable returning [Problem Details](https://www.rfc-editor.org/rfc/rfc7807) in unhandled exception responses.

* [Problem Details - 2](problem-details-2)

  This example shows how customize returned [Problem Details](https://www.rfc-editor.org/rfc/rfc7807) in unhandled exception responses.

* [Problem Details - 3](problem-details-3)

  This example shows how customize [Problem Details](https://www.rfc-editor.org/rfc/rfc7807) by manipulating `IProblemDetailsService`.


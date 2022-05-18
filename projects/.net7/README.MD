# .NET 7 (5)

Samples in this section require .NET 7 Preview 4. You can download it from [here](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).

* [Infer dependency from action parameter](mvc-infer-dependency-from-action)

  There is no need for `[FromServices]` attribute anymore to inject a dependency to your action method.

* [Typed Results - 1](typed-results-1)
  
  `Microsoft.AspNetCore.Http.TypedResults` static class is the “typed” equivalent of the existing `Microsoft.AspNetCore.Http.Results` class.

* [WithOpenApi - 1](openapi-1)

  This sample demonstrate the usage of `WithOpenApi` extension method available in  `Microsoft.AspNetCore.OpenApi` to customize OpenAPI operation information.

* [Results<> Union Type](openapi-2)

  `Results<TResult1, TResult2, TResultN>` provides better description of the result of the operation that OpenAPI/Swagger can use in describing the API.

* [Route Group - 1](map-group-1)

  `MapGroup()` extension methods allow grouping of endpoints with a common prefix. It also allow group metadata to be attached to the group.
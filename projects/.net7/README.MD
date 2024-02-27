# .NET 7 (40)

Samples in this section require .NET 7. You can download it from [here](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).

* [Infer dependency from action parameter](mvc-infer-dependency-from-action)

  There is no need for `[FromServices]` attribute anymore to inject a dependency to your action method.

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


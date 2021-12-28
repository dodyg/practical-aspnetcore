# Diagnostics (6)

  * [Welcome Page](/projects/diagnostics/diagnostics-1)

    Simply show a welcome page to indicate that the app is working properly. This sample does not use a startup class simply because it's just a one line code.

  * [Developer Exception Page](/projects/diagnostics/diagnostics-2)

    Show any unhandled exception in a nicely formatted page with error details. Only use this in development environment!

  * [Custom Global Exception Page](/projects/diagnostics/diagnostics-3)

    Use ```IExceptionHandlerFeature``` feature provided by ```Microsoft.AspNetCore.Diagnostics.Abstractions``` to create custom global exception page.

  * [Custom Global Exception Page - 2](/projects/diagnostics/diagnostics-4)

    Similar to the previous one except that that we use the custom error page defined in separate path.

  * [Status Pages](/projects/diagnostics/diagnostics-5)

    Use ```UseStatusCodePagesWithRedirects```.  **Beware:** This extension method handles your 5xx return status code by redirecting it to a specific url. It will not handle your application exception in general (for this use ```UseExceptionHandler``` - check previous samples).

  * [Middleware Analysis](/projects/diagnostics/diagnostics-6)

    Here we go into the weeds of analysing middlewares in your request pipeline. This is a bit complicated. It requires the following packages:

    * ```Microsoft.AspNetCore.MiddlewareAnalysis```
    * ```Microsoft.Extensions.DiagnosticAdapter```
    * ```Microsoft.Extensions.Logging.Console```

dotnet6
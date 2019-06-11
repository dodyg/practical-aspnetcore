# ASP.NET Core 2.2

## What's new in ASP.NET Core 2.2 (14)

  All the samples in this section requires ASP.NET Core 2.2 (`2.2.300`). Download it [here](https://www.microsoft.com/net/download/dotnet-core/2.2).
  
  * [Endpoint Routing](endpoint-routing)

    Enable Endpoint Routing for your MVC Core app. You will gain a faster performance and more functionalities regarding routing. 

  * [Endpoint Routing - GetUriByAction](endpoint-routing-2)

    Use the `LinkGenerator` singleton and its `GetUriByAction` method to generate a link to an Action. It will respect the convention used by MVC, which is, in this example, `app.UseMvcWithDefaultRoute();`.

  * [Endpoint Routing - GetUriByAction - 2](endpoint-routing-3)

    Use the `LinkGenerator` singleton and its `GetUriByAction` method to generate a link to an Action. This sample uses various combination of `Route` and `HttpGet` attributes to generate various links.
    
  * [Endpoint Routing - GetUriByAction - 3](endpoint-routing-4)

    Show how to deal with route with values using `LinkGenerator.GetUriByAction`. If you don't deal with the values, the link generator won't generate the link.

  * Endpoint Routing - GetTemplateByAction

    __This sample is no longer relevant__. `LinkGenerator.GetTemplateByAction` has been removed from ASP.NET Core 2.2 final version.
    Demonstrate on how to obtain route template from an existing Action using `LinkGenerator.GetTemplateByAction` and generate path using the information.

  * [Endpoint Routing - GetPathByAction](endpoint-routing-6)

    Show how to deal with route with values using `LinkGenerator.GetPathByAction`. If you don't deal with the values, the link generator won't generate the link.

  * [Parameter Transformer ](parameter-transformer)
     
    Use Parameter Transformer to control the creation of route token `[area]`, `[controller]` and `[action]`. In this example we use it on `[controller]` and `[action]`.
   
  * [Health Check - Check URL](health-check)

    Show the simplest way to use health check functionality using `app.UseHealthChecks`.

  * [Health Check - Check URL - 2](health-check-2)

    Customize the message returned by `app.UseHealthChecks`.

  * [Health Check - Check URL - 3](health-check-3)

    Start implementing `IHealthCheck` to provide status information for the health check service. In this example, it will always return failure because we just throw an exception in the implementation. You will see how the health check handles an unhandled exception.

  * [Health Check - Check URL - 4](health-check-4)

    Implement a `IHealthCheck` that check the status of a url. This is the first version of the check so it is primitive but it is also easier to understand. We will go to a more sophisticated multi check in the next examples.

  * [Health Check - Check URL - 5](health-check-5)

    Similar to the previous example except that now there are two checks, one fails and one successful. 

  * [Health Check - Check URL - 6](health-check-6)

    Similar to the previous example except that one of the check reports "Degraded" status by using `context.Registration.FailureStatus = HealthStatus.Degraded;`.

  * [New Redis Caching Package](new-redis-caching-package)

    Instead of using `Microsoft.Extensions.Caching.Redis`, use the new `Microsoft.Extensions.Caching.StackExchangeRedis`. The former will be deprecated in .NET Core 3.0 (https://github.com/aspnet/Announcements/issues/322). 

  * [Allow or disallow Synchronous IO in your pipeline](allow-sync-io)

    This sample demonstrates the impact of setting `KestelSeverOptions.AllowSynchronousIO` to false, which disallows any synchronous IO in your request pipeline. By default this option is set to true.


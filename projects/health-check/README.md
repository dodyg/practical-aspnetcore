# Health Check (6)

  * [Health Check - Check URL](health-check-1)

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

dotnet6
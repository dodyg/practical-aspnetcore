# Generic Host (9)

  Generic Host is an awesome way to host all sort of long running tasks and applications, e.g. messaging, background tasks, etc.

  * [Configure Logging](/projects/generic-host/generic-host-configure-logging)

    Configure logging for your Generic Host. This technique will be used in the subsequent samples.

  * [Hello World](/projects/generic-host/generic-host-1)

    This is the hello world equivalent of a Generic Host service.

  * [Hello World using Console Lifetime](/projects/generic-host/generic-host-2)

    Use `UseConsoleLifetime` implicitly. 

  * [Startup and Shutdown order](/projects/generic-host/generic-host-3)

    Demonstrates the startup and shutdown order of hosted services.

  * [Start and stop the host](/projects/generic-host/generic-host-4)

    Demonstrates starting and stopping the host programmatically.

  * [A service with timed execution](/projects/generic-host/generic-host-5)

    Demonstrate processing a task on a regular interval using `Task.Delay`.

  * [Configure Host using Dictionary](/projects/generic-host/generic-host-configure-host)

    Demonstrate the way to inject configuration values to the host using Dictionary.

  * [Configure Environment](/projects/generic-host/generic-host-environment)

    Set your environment using `EnvironmentName.Development` or `EnvironmentName.Production` or `EnvironmentName.Staging`.

  * [Listen to IHostApplicationLifetime events](/projects/generic-host/generic-host-ihostapplicationlifetime)

    Inject `IHostApplicationLifetime` and listen to `ApplicationStarted`, `ApplicationStopping` and `ApplicationStopped` events. This is important to allow services to be shutdown gracefully. The shutdown process blocks until `ApplicatinStopping` and `ApplicationStopped` events complete.

dotnet6
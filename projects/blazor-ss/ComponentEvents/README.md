# Component Events

There are many ways for Parent component to communicate to Children components in Blazor and vice versa. However there is no straightforward way to manage communication between components that are located in different hierarchy. We are using an intermediary [Scoped lifetime](https://docs.microsoft.com/en-us/aspnet/core/blazor/dependency-injection?view=aspnetcore-3.1) AppState object here to faciliate the communication. 


> However, the Blazor Server hosting model supports the Scoped lifetime. In Blazor Server apps, a scoped service registration is scoped to the connection. For this reason, using scoped services is preferred for services that should be scoped to the current user, even if the current intent is to run client-side in the browser.
[Scoped lifetime](https://docs.microsoft.com/en-us/aspnet/core/blazor/dependency-injection?view=aspnetcore-3.1)

The AppState object needs to be in Scoped lifetime so it exists only for your current session. If you make it Singleton your AppState will be modified by other users. 
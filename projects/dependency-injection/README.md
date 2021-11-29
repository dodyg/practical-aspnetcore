# Dependency Injection (4)

  ASP.NET Core lives and dies by DI. It relies on `Microsoft.Extensions.DependencyInjection` library. 

  * [Dependency Injection 1 - The basic](/projects/dependency-injection/dependency-injection-1)

    Demonstrate the three lifetime registrations for the out of the box DI functionality: singleton (one and only forever), scoped (one in every request) and transient (new everytime).

  * [Dependency Injection 2 - IServiceProvider](/projects/dependency-injection/dependency-injection-2)

    `IServiceProvider` is a service locator. It allows you to obtain objects from the dependency injection system.

  * [Dependency Injection 3 - Easy registration](/projects/dependency-injection/dependency-injection-3)
  
    Register all objects configured by classes that implements a specific interface (`IBootstrap` in this example). This is useful when you have large amount of classes in your project that needs registration. You can register them near where they are (usually in the same folder) instead of registering them somewhere in a giant registration function.

  * [Lazy Type](/projects/dependency-injection/dependency-injection-4)

    This shows how to register [Lazy](https://docs.microsoft.com/en-us/dotnet/api/system.lazy-1?view=net-5.0) types in the built in Dependency Injection. 

dotnet6
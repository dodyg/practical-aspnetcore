# IServiceProvider

`IServiceProvider` is a service locator. It allows you to obtain objects from the dependency injection system. It is considered a bad practice in common scenario.

It is better to be explicit of your dependency in your constructor e.g. `MyObject(ILogger<MyObject> logger, IStringLocalizer<MyObject>)` than using `MyObject(IServiceProvider provider)` and obtain those objects via the provider.

However when you encounter a situation where using a service locator is needed, `IServiceProvider` is there for you.

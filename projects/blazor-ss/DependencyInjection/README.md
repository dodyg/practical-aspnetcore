# Dependency Injection

Now that all your requests are going through a single two way websocket, it is hard to figure out what a request means. This will have impact on the usage of the standard built-in dependency injection concept of 'Scoped' vs 'Transient' (Singleton would behave as expected). 

Run the sample on `DependencyInjection` using `dotnet watch`.
# Middleware (14)

  We will explore all aspect of middleware building in this section.

  * [Middleware 0](/projects/middleware/middleware-0)

    ASP.NET Core is built on top of pipelines of functions called middleware. This sample shows the basic outline on how they work. 
    
    We are using ```IApplicationBuilder Use```, an extension method for adding middleware and ```IApplicationBuilder Run```.

  * [Middleware 1](/projects/middleware/middleware-1)
   
    This example shows how to pass information from one middleware to another using `HttpContext.Items`.

  * [Middleware 2](/projects/middleware/middleware-2)
   
    As a general rule, only one of your Middleware should write to Response in an execution path. This sample shows how to work around this by buffering the Response.

    e.g.

    If path `/` involves the execution of Middleware 1, Middleware 2 and Middleware 3, only one of these should write to Response.

  * [Middleware 3](/projects/middleware/middleware-3)
   
    This is the simplest middleware class you can create. 

  * [Middleware 4](/projects/middleware/middleware-4)
   
    Use `app.Map` (`MapMiddleware`) to configure your middleware pipeline to respond only on specific url path.

  * [Middleware 5](/projects/middleware/middleware-5)
   
    Nested `app.Map` (show `Request.Path` and `Request.PathBase`).

  * [Middleware 6](/projects/middleware/middleware-6)
   
    Use `app.MapWhen`(`MapWhenMiddleware`) and Nested `app.Map` (show `Request.Path` and `Request.PathBase`).

  * [Middleware 7](/projects/middleware/middleware-7)
   
    Use `MapMiddleware` and `MapWhenMiddleware` directly without using extensions (show `Request.Path` and `Request.PathBase`).

  * [Middleware 8](/projects/middleware/middleware-8)
   
    Demonstrate the various ways you can inject dependency to your middleware class *manually*. 

  * [Middleware 9](/projects/middleware/middleware-9)
   
    Demonstrate how to ASP.NET Core built in DI (Dependency Injection) mechanism to provide dependency for your middleware.

  * [Middleware 10](/projects/middleware/middleware-10)
   
    Demonstrate that a middleware is a singleton.

  * [Middleware 11](/projects/middleware/middleware-11)
   
    This sample is similar to `Middleware 10` except that this one implement `IMiddleware` to create Factory-based middleware activation. This means that you can create a middleware that is not a singleton (either Transient or Scoped). 

  * [Middleware 12](/projects/middleware/middleware-12)

    Contrast the usage of `MapWhen` (which branch execution) and `UseWhen` (which doesn't branch execution) in configuring your Middleware.

  * [Middleware 13](/projects/middleware/middleware-13)

    Demonstrate how to implement basic error handling mechanism and how to return error object (same for all api exceptions).

dotnet6
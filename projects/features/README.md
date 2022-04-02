# Features (11)

  Features are collection of objects you can obtain from the framework at runtime that serve different purposes.

  * [Server Addresses Feature](/projects/features/features-server-addresses)

    Use this Feature to obtain a list of urls that your app is responding to.

  * [Server Addresses Feature - 2](/projects/features/features-server-addresses-2)

    Use `IServer` interface to access server addressess when you don't have access to `IApplicationBuilder`. 

  * [Request Feature](/projects/features/features-server-request)

    Obtain details of a current request. It has some similarity to HttpContext.Request. They are not equal. `HttpContext.Request` has more properties.  

  * [Connection Feature](/projects/features/features-connection)

    Use `IHttpConnectionFeature` interface to obtain local ip/port and remote ip/port. 

  * [Custom Feature](/projects/features/features-server-custom)

    Create your own custom Feature and pass it along from a middleware. 

  * [Custom Feature - Override](/projects/features/features-server-custom-override)

    Shows how you can replace an implementation of a Feature with another within the request pipeline.

  * [Request Culture Feature](/projects/features/features-request-culture)

    Use this feature to detect the culture of a web request through `IRequestCultureFeature`. 

  * [Session Feature](/projects/features/features-session)

    Use session within your middlewares. This sample shows a basic usage of in memory session. 

  * [Session Feature with Redis](/projects/features/features-session-redis-2)

    Use session within your middlewares. This sample uses Redis to store session data. 

  * [Maximum Request Body Size Feature](/projects/features/features-max-request-body-size)

    Use this feature to read and set maximum HTTP Request body size.

  * [IHttpResponseBodyFeature](/projects/features/features-http-body-response)

    This new Feature interface consolidate previous version's three response body APIs into one

dotnet6
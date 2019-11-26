# Endpoint Routing

* [Endpoint Routing - Razor Page](/projects/endpoint-routing/new-routing)

  ASP.NET Core 3 allows more control on how to organize your endpoints using `app.UseEndpoints`. In this example, we just map Razor Pages routes and nothing else.

* [Endpoint Routing - MVC](/projects/endpoint-routing/new-routing-2)

  ASP.NET Core 3 allows more control on how to organize your endpoints using `app.UseEndpoints`. In this example, we just map MVC routes (attribute routing only, not convention routing) and nothing else.

* [Endpoint Routing - MVC with default route](/projects/endpoint-routing/new-routing-3)

  Map MVC routes with default `{controller=Home}/{action=Index}/{id?}` set up.

* [Endpoint Routing - RequestDelegate](/projects/endpoint-routing/new-routing-4)

  This example shows how to use `RequestDelegate` directly in `app.UseEndpoints` for `GET` operation using `MapGet`. `MapPost`, `MapPut`, and `MapDelete` are also available for use.

  This allow the creation of very minimalistic web services apps.

* [Endpoint Routing - RequestDelegate](/projects/endpoint-routing/new-routing-5)

  This example shows how to use `RequestDelegate` directly in `app.UseEndpoints` using `Map`.

* [Endpoint Routing - Interrogate available endpoints](/projects/endpoint-routing/new-routing-6)

  This example shows how to list all available endpoints in your app.

* [Endpoint Routing - RequestDelegate with HTTP verb filter](/projects/endpoint-routing/new-routing-7)

  This example shows how to use `RequestDelegate` directly in `app.UseEndpoints` using `MapMethods` that filter request based on one or more HTTP verbs.

* [Endpoint Routing - Static file fallback](/projects/endpoint-routing/new-routing-8)

  Return a static page when your request does not match anything else using `MapFallbackToFile`.

* [Endpoint Routing - Razor Page fallback](/projects/endpoint-routing/new-routing-9)

  Return a Razor Page when your request does not match anything else using `MapFallbackToPage`.

* [Endpoint Routing - Obtaining an Endpoint from your Middleware](/projects/endpoint-routing/new-routing-10)

  Use the brand new `HttpContext.GetEndPoint` extension method to examine the current endpoint that is being executed.

* [Endpoint Routing - How to obtain metadata in an Endpoint from a Razor page](/projects/endpoint-routing/new-routing-11)

  Use the brand new `EndPoint.Metadata.GetMetadata<>()` to get values from attributes at your Razor Page.

* [Endpoint Routing - Obtaining an Endpoint metadata from your Razor Page depending on the request method](/projects/endpoint-routing/new-routing-12)

  Unlike in MVC, you can't use `Attribute` from the method of a Razor Page. You can only use it from the Model class. This makes getting obtaining the appropriate metadata for each request require an extra step.

* [Endpoint Routing - Obtaining an Endpoint metadata from your MVC Controller](/projects/endpoint-routing/new-routing-13)

  Obtain Endpoint metadata from MVC Controller's Action methods.

* [Endpoint Routing - Obtaining Endpoint feature via IEndpointFeature](/projects/endpoint-routing/new-routing-14)

  Use `HttpContext.Features.Get<IEndpointFeature>();` to obtain `Endpoint` information for a given Middleware. You can accomplish the same thing using `HttpContext.GetEndpoint`.

* [Endpoint Routing - Attaching Metadata information to your inline Middleware](/projects/endpoint-routing/new-routing-15)

  Use `IEndpointConventionBuilder.WithMetadata` to attach metadata information to your inline Middleware.

* [Endpoint Routing - Map Areas by Convention](/projects/endpoint-routing/new-routing-16)

  Use `IEndpointRouteBuilder.MapAreaControllerRoute` to configure routing for your areas.

* [Endpoint Routing - enable MVC but without Views support](/projects/endpoint-routing/new-routing-17)

  Use `services.AddControllers` to provide MVC without Views supports. Razor Pages is not available. Perfect for Web APIs.

* [Endpoint Routing - enable MVC but with Views support but without Razor Page](/projects/endpoint-routing/new-routing-18)

  Use `services.AddControllersWithViews();` to provide MVC with Views supports. Razor Page is not available. So this similar to the "classic" MVC configuration.

* [Endpoint Routing - enable Razor Pages with MVC API support](/projects/endpoint-routing/new-routing-19)

  Use `services.AddRazorPages()` add supports for Razor Pages and MVC API.

* [Endpoint Routing - Convention based Routing](/projects/endpoint-routing/new-routing-20)
  
  Use `IEndpointRouteBuilder.MapControllerRoute` to configure convention based routing.

* [Endpoint Routing - A new way to map health check](/projects/endpoint-routing/new-routing-21)
  
  Use `IEndpointRouteBuilder.MapHealthChecks` to configure health check instead of `IApplicationBuilder.UseHealthChecks`.

* [Endpoint Routing - Configure Endpoints default on Kestrel](/projects/endpoint-routing/new-routing-22)

  We configure `KestrelServerOptions.ConfigureEndpointDefaults` so the Endpoints will run only on HTTP/2.

* [Endpoint Routing - Host Matching](/projects/endpoint-routing/new-routing-23)

  This example demonstrates on how to configure your endpoint to respond to a request from a specific host. In this example, GET `/` returns a different result depending whether you access it from `localhost:8111` and `localhost:8112`.

* [Endpoint Routing - Host Matching 2](/projects/endpoint-routing/new-routing-24)

  This produces the same exact effect as the [previous example above - Host Matching](/projects/endpoint-routing/new-routing-23) except that here we use `IEndpointConventionBuilder.WithMetadata` and `HostAttribute` instead of `IEndpointConventionBuilder.RequireHost`.

* [Endpoint Routing - Handle MVC routing dynamically](/projects/endpoint-routing/new-routing-25)

  This example shows how to handle MVC routing dynamically using `MapDynamicControllerRoute` and `DynamicRouteValueTransformer`.

* [Endpoint Routing - Handle Razor Pages routing dynamically](/projects/endpoint-routing/new-routing-26)

  This example shows how to handle Razor Pages routing dynamically using `MapDynamicPageRoute` and `DynamicRouteValueTransformer`.
  
* [Endpoint Routing - Setup to Razor Pages areas](/projects/endpoint-routing/new-routing-28)

  This example shows how to create Razor Pages areas.

* [Endpoint Routing - Map a route to a Razor Pages in an area](/projects/endpoint-routing/new-routing-27)

  Map a route to a Razor Pages located in an Area using `Conventions.AddAreaPageRoute`. 

* [Endpoint routing - setup a fallback page in a Razor Pages area](/projects/endpoint-routing/new-routing-29)

  This sample shows you how to setup a fallback page located in a Razor Pages area. 

* [Endpoint routing - serve different fallback pages depending on route pattern match](/projects/endpoint-routing/new-routing-30)

  This sample shows how to return different fallback page located in areas depending on the route pattern that matches the request.
  
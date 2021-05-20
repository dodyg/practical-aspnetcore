# .NET 6 (9)

This section has for .NET 6 new functionalities. All these samples require .NET 6 Preview 4 nightly build(`6.0.100-preview.4.21219.6`) which you can obtain [here](https://github.com/dotnet/installer).

* [Hello World](hello-world)
  
  This is Hello World ASP.NET Core 6 app using the new `WebApplication` instead of `IHostBuilder`.

* [Map](map)

  This is a brand new feature that allows the creation of Web API without using Controllers.
  
* [Accessing HttpContext in Map](map-2)

  This sample shows how to access HttpContext in `Map`.

* [Accessing Objects in Map](map-3)

  This sample shows how to access objects `Map`.

* [MapPost with JSON model binder](map-post)

  This sample shows how JSON model binder works in `MapPost`.

* [MapPost with JSON model binder - 2](map-post-2)

  This sample shows how JSON model binder works in `MapPost` using static method.

* [MapMethods with JSON model binder](map-methods)

  This sample shows how to handle multiple HTTP Verbs request using `MapMethods`.

* [WebApplication - Welcome Page](web-application)

  This uses the new minimalistic hosting code `WebApplication` and show ASP.NET Core welcome page.

* [WebApplication - UseFileServer](web-application-2)

  This uses the new minimalistic hosting code `WebApplication` and server default static files.
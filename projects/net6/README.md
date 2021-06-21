# .NET 6 (19)

This section has for .NET 6 new functionalities. All these samples require .NET 6 Preview 5(`6.0.100-preview.5.21302.13`) which you can obtain [here](https://dotnet.microsoft.com/download/dotnet/6.0).

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

## Minimal Hosting Default Configuration

  This is a set of samples that demonstrates things that you can do with the default configuration using `WebApplication.Create()` before you have to resort to `WebApplication.CreateBuilder()`. 

  * [WebApplication - Welcome Page](web-application)

    This uses the new minimalistic hosting code `WebApplication` and show ASP.NET Core welcome page.

  * [WebApplication - UseFileServer](web-application-2)

    This uses the new minimalistic hosting code `WebApplication` and server default static files.

  * [WebApplication - Default Logger](web-application-3)

    `WebApplication.Logger` is available for use immediately without any further configuration. However the default logger is not available via DI.

  * [WebApplication - Default Urls](web-application-4)

    This sample shows the default Urls that the built in Kestrel web server listens to.
    
  * [WebApplication - Set Url and Port](web-application-5)

    This sample shows how to set the Kestrel web server to listen to a specific Url and port.

  * [WebApplication - Application lifetime events](web-application-6)

    In this sample we learn how to respond to the application lifetime events.

  * [WebApplication - Configuration](web-application-7)

    This sample list all the information available in the `Configuration` property. 

  * [WebApplication - Configuration as JSON](web-application-8)

    This sample list all the information available in the `Configuration` property and return it as JSON. WARNING: Do not use this in your application. It is a terrible idea. Do not expose your configuration information over the wire. This sample is just to demonstrate a technique. 

In most cases using ```WebApplication``` isn't enough because you need to configure additional services to be used in your system. This is where ```WebApplicationBuilder``` comes. It allows you to configure services and other properties.

  * [WebApplicationBuilder - enable MVC](web-application-builder)

    This sample shows how to enable MVC.

* [WebApplicationBuilder - enable Razor Pages](web-application-builder-2)

    This sample shows how to enable Razor Pages.

## Reverse Proxy - YARP
This section showcases creating reverse proxy using YARP package.
* [Basic Demo](yarp/basic-demo)

  Basic demo with simple YARP routing.

## Writable JSON DOM

* [Primitives](json/json-12)
  
  This sample shows how to parse and access number, string and an array values from JSON string.
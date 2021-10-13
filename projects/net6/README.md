# .NET 6 (48)

This section has for .NET 6 new functionalities. All these samples require .NET 6RC2(`6.0.100-rc.2.21505.57`) which you can obtain [here](https://dotnet.microsoft.com/download/dotnet/6.0).

* [Hello World](hello-world)
  
  This is Hello World ASP.NET Core 6 app using the new `WebApplication` instead of `IHostBuilder`.

## Minimal Routing

In these examples we are using the familiar `Host.CreateDefaultBuilder(args)` host configuration style to emphasis that the new Minimal Routing feature works in both existing style and also in the new minimal hosting API.

* [Map](map)

  This is a brand new feature that allows the creation of Web API without using Controllers.
  
* [Accessing HttpContext in Map](map-2)

  This sample shows how to access HttpContext in `Map`.

* [Accessing Objects in Map](map-3)

  This sample shows how to access objects `Map`.

* [Setup Open API support](map-4)

  This sample shows how to setup Open API support with Swashbuckle.

* [HttpRequest injection](map-5)

  This sample shows how to use `HttpRequest` directly without relying on `HttpContext`.

* [IEndpointRouteBuilder](map-6)

  This sample shows the use of `IEndPointRouteBuilder` to organize code.

* [MapPost with JSON model binder](map-post)

  This sample shows how JSON model binder works in `MapPost`.

* [MapPost with JSON model binder - 2](map-post-2)

  This sample shows how JSON model binder works in `MapPost` using static method.

* [MapMethods with JSON model binder](map-methods)

  This sample shows how to handle multiple HTTP Verbs request using `MapMethods`. In this example we send 'PUT', 'POST' and 'PATCH' requests.

## Route Constraints

  * [Route Constraints - int, min, max and range](route-constraints-int)
  
    This example shows route constraint for integer, the minimum and maximum value, and the range.

  * [Route Constraints - decimal, float and double](route-constraints-decimal)
    
    This example shows route constraint for decimal, float and double.

## Link Generator

 * [Link Generator - Generate path from a route name](link-generator-path-by-route-name)
    
   This sample shows how to generate a url path from a route name.
   
 * [Link Generator - Generate path from an inferred route name](link-generator-path-by-route-name-inferred)
    
   This sample shows how to generate a url path from an inferred route name.

## Parameter Binding

 * [Route binding - implicit](parameter-binding-route-implicit)

   This example shows usage of implicit route parameter binding of `int` and `string` types.
      
## WebApplication - Minimal Hosting

  This is a set of samples that demonstrates things that you can do with the default configuration using `WebApplication.Create()` before you have to resort to `WebApplication.CreateBuilder()`. 

  * [WebApplication - Welcome Page](web-application-welcome-page)

    This uses the new minimalistic hosting code `WebApplication` and show ASP.NET Core welcome page.

  * [WebApplication - Default Logger](web-application-logging)

    `WebApplication.Logger` is available for use immediately without any further configuration. However the default logger is not available via DI.

  * [WebApplication - Application lifetime events](web-application-6)

    In this sample we learn how to respond to the application lifetime events.

  * [WebApplication - Middleware](web-application-middleware)

    In this sample we learn how to use middleware by creating two simple middleware. 

  * [WebApplication - Middleware Pipelines](web-application-middleware-pipeline)

    In this sample we learn how to use `UseRouter` and `MapMiddlewareGet` to compose two different middleware pipelines. 

  * [WebApplication - Middleware Pipelines 2](web-application-middleware-pipeline-2)

    In this sample we use `EndpointRouteBuilderExtensions.Map` and `IEndPointRouteBuilder.CreateApplicationBuilder` to build two separate middleware pipelines. 

### Configuration

  * [WebApplication - Configuration](web-application-configuration)

    This sample list all the information available in the `Configuration` property. 

  * [WebApplication - Configuration as JSON](web-application-configuration-json)

    This sample list all the information available in the `Configuration` property and return it as JSON. WARNING: Do not use this in your application. It is a terrible idea. Do not expose your configuration information over the wire. This sample is just to demonstrate a technique. 

### Server

  * [WebApplication - Default Urls](web-application-server-default-urls)

    This sample shows the default Urls that the web server listens to.
    
  * [WebApplication - Set Url and Port](web-application-server-specific-url-port)

    This sample shows how to set the Kestrel web server to listen to a specific Url and port.

  * [WebApplication - Listen to multiple Urls and Ports](web-application-server-multiple-urls-ports)

    This sample shows how to set the Kestrel web server to listen to multiple Urls and Ports.

  * [WebApplication - Set Port from an Environment Variable](web-application-server-port-env-variable)

    This sample shows how to set the Kestrel web server to listen to a specific port set from an environment variable.

  * [WebApplication - Set Urls and Port via ASPNETCORE_URLS Environment Variable](web-application-server-aspnetcore-urls)

    This sample shows how to set the Kestrel web server to listen to a specific url and port via `ASPNETCORE_URLS` environment variable.

  * [WebApplication - Listening to all interfaces on a specific port](web-application-server-listen-all)

    This sample shows how to set the Kestrel web server to listen to all IPs (IP4/IP6) on a specific port.

### Standard ASP.NET Core Middlewares

  * [WebApplication - UseFileServer](web-application-use-file-server)

    This uses the new minimalistic hosting code `WebApplication` and server default static files.

  * [WebApplication - UseWebSockets](web-application-use-web-sockets)

    This sample shows how to use WebSockets by creating a simple echo server.

## WebApplicationBuilder - Minimal Hosting

In most cases using ```WebApplication``` isn't enough because you need to configure additional services to be used in your system. This is where ```WebApplicationBuilder``` comes. It allows you to configure services and other properties.

  * [WebApplicationBuilder - enable MVC](web-application-builder-mvc)

    This sample shows how to enable MVC.

  * [WebApplicationBuilder - enable Razor Pages](web-application-builder-razor-pages)

    This sample shows how to enable Razor Pages.

  * [WebApplicationBuilder - change environment](web-application-builder-change-environment)

    This sample shows how to change the environment via code.

  * [WebApplicationBuilder - change logging minimum level](web-application-builder-logging-set-minimum-level)

    This sample shows how to set logging minimum level via code.

### WebApplicationOptions

  Use ```WebApplicationOptions``` to configure initial values of the ```WebApplicationBuilder``` object.

  * [WebApplicationOptions - set environment](web-application-options-set-environment)

    This sample shows how to set the environment.  

  * [WebApplicationOptions - change the content root path](web-application-options-change-content-root-path)

    This sample shows how to change the content root path of the application.

## Reverse Proxy - YARP
This section showcases creating reverse proxy using YARP package.
* [Basic Demo](yarp/basic-demo)

  Basic demo with simple YARP routing.

## Writable JSON DOM

   [Design document for the Writable JSON API](https://github.com/dotnet/designs/blob/main/accepted/2020/serializer/WriteableDomAndDynamic.md)

* [Primitives](json/json-12)
  
  This sample shows how to parse and access number, string and an array values from JSON string.

* [Object](json/json-13)

  This sample shows how to parse and access objects from JSON string. We will be using `JsonObject` as well.

* [Finding a node using LINQ](json/json-14)

  This sample shows how to find a node based on of its value using LINQ.

* [Finding a node using LINQ 2](json/json-15)

  This sample shows how to find a node based on two of its values (a string and an array) using LINQ.

* [Finding a node using LINQ 3](json/json-16)

  This sample shows how to find a node based of an absence of a property using LINQ.

* [Finding a node using LINQ 4](json/json-17)

  In this example we are trying to find a node in an array that has a specific value on its array property.

* [Construct a JSON document](json/json-18)

  This sample shows how to construct a JSON document using `JsonObject`.

* [Construct a JSON document](json/json-19)

  This sample shows how to construct a JSON document using `JsonArray`.

* [Update a JSON document](json/json-20)

  This sample shows how to update properties of a JSON document.

* [Delete elements in a JSON document](json/json-21)

  This example shows how to update remove an object property and an element in an array.

* [Add items into a JSON array](json/json-22)
  
  This example shows how to add items at the first position of an array and at the last position.

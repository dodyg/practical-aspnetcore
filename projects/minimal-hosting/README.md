# Minimal Hosting (23)

## WebApplication (6)

  This is a set of samples that demonstrates things that you can do with the default configuration using `WebApplication.Create()` before you have to resort to `WebApplication.CreateBuilder()`. 

  * [WebApplication - Welcome Page](web-application-welcome-page)

    This uses the new minimalistic hosting code `WebApplication` and show ASP.NET Core welcome page.

  * [WebApplication - Default Logger](web-application-logging)

    `WebApplication.Logger` is available for use immediately without any further configuration. However the default logger is not available via DI.

  * [WebApplication - Application lifetime events](web-application-lifetime-events)

    In this sample we learn how to respond to the application lifetime events.

  * [WebApplication - Middleware](web-application-middleware)

    In this sample we learn how to use middleware by creating two simple middleware. 

  * [WebApplication - Middleware Pipelines](web-application-middleware-pipeline)

    In this sample we learn how to use `UseRouter` and `MapMiddlewareGet` to compose two different middleware pipelines. 

  * [WebApplication - Middleware Pipelines 2](web-application-middleware-pipeline-2)

    In this sample we use `EndpointRouteBuilderExtensions.Map` and `IEndPointRouteBuilder.CreateApplicationBuilder` to build two separate middleware pipelines. 

### Configuration (2)

  * [WebApplication - Configuration](web-application-configuration)

    This sample list all the information available in the `Configuration` property. 

  * [WebApplication - Configuration as JSON](web-application-configuration-json)

    This sample list all the information available in the `Configuration` property and return it as JSON. WARNING: Do not use this in your application. It is a terrible idea. Do not expose your configuration information over the wire. This sample is just to demonstrate a technique. 

### Server (6)

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

### Standard ASP.NET Core Middlewares (2)

  * [WebApplication - UseFileServer](web-application-use-file-server)

    This uses the new minimalistic hosting code `WebApplication` and server default static files.

  * [WebApplication - UseWebSockets](web-application-use-web-sockets)

    This sample shows how to use WebSockets by creating a simple echo server.

## WebApplicationBuilder - Minimal Hosting (5)

In most cases using ```WebApplication``` isn't enough because you need to configure additional services to be used in your system. This is where ```WebApplicationBuilder``` comes. It allows you to configure services and other properties.

  * [WebApplicationBuilder - enable MVC](web-application-builder-mvc)

    This sample shows how to enable MVC.

  * [WebApplicationBuilder - enable Razor Pages](web-application-builder-razor-pages)

    This sample shows how to enable Razor Pages.

  * [WebApplicationBuilder - change environment](web-application-builder-change-environment)

    This sample shows how to change the environment via code.

  * [WebApplicationBuilder - change logging minimum level](web-application-builder-logging-set-minimum-level)

    This sample shows how to set logging minimum level via code.

  * [WebApplicationBuilder - change web root folder](web-application-builder-change-default-web-root-folder)

    This samples shows how to use `WebApplicationOptions.WebRootPath` to change the default web root folder.

### WebApplicationOptions (2)

  Use ```WebApplicationOptions``` to configure initial values of the ```WebApplicationBuilder``` object.

  * [WebApplicationOptions - set environment](web-application-options-set-environment)

    This sample shows how to set the environment.  

  * [WebApplicationOptions - change the content root path](web-application-options-change-content-root-path)

    This sample shows how to change the content root path of the application.

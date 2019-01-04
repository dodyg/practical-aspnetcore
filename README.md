# 216 samples for ASP.NET Core 2.1 and 2.2 fundamentals (updated almost daily - except during Summer)

If you are studying ASP.NET Core, I am lurking on this **[Gitter Channel](https://gitter.im/DotNetStudyGroup/aspnetcore)**.

## Welcome

The goal of this project is to enable .NET programmers to learn the new ASP.NET Core stack from the ground up directly from code. There is so much power in the underlying ASP.NET Core stack. Don't miss them!

I highly recommend using [Visual Studio Code](https://code.visualstudio.com/) to play around with these samples but it is not required. You can use Visual Studio 2017 as well.

Note: If you encounter problem with downloading packages or Nuget, try the following command `nuget.exe locals -clear all`.

ASP.NET Core API Browser is also very [handy](https://docs.microsoft.com/en-us/dotnet/api/?view=aspnetcore-2.2). 

### Security 

There is a new section dedicated to [all security related topics (1 sample)](/projects/security) for ASP.NET Core.

### MVC, SignalR and Blazor

There are dedicated samples for **ASP.NET Core MVC 2.1** [here (40 samples)](/projects/mvc/README.md), **Razor Pages 2.2** [here (1 sample)](/projects/razor-pages/README.md), **ASP.NET Core SignalR 2.1** [here (1 sample)](/projects/signalr/README.md) and **Blazor** [here (12 samples)](/projects/blazor/README.md). The rest of projects here are for ASP.NET Core only.

## How to run these samples

To run these samples, simply open your command line console,  go to each folder and execute `dotnet watch run`.

Most of the examples here uses `Microsoft.AspNetCore` package which is a package consisted of 

```
  Microsoft.AspNetCore.Diagnostics
  Microsoft.AspNetCore.HostFiltering
  Microsoft.AspNetCore.Hosting
  Microsoft.AspNetCore.Routing
  Microsoft.AspNetCore.Server.IISIntegration
  Microsoft.AspNetCore.Server.Kestrel
  Microsoft.AspNetCore.Server.Kestrel.Https
  Microsoft.Extensions.Configuration.CommandLine
  Microsoft.Extensions.Configuration.EnvironmentVariables
  Microsoft.Extensions.Configuration.FileExtensions
  Microsoft.Extensions.Configuration.Json
  Microsoft.Extensions.Configuration.UserSecrets
  Microsoft.Extensions.Logging
  Microsoft.Extensions.Logging.Configuration
  Microsoft.Extensions.Logging.Console
  Microsoft.Extensions.Logging.Debug
```

When an example requires packages that are not listed here, it will be added to the project file.

## What's new in ASP.NET Core 2.2 (13)

  All the samples in this section requires ASP.NET Core 2.2 (`2.2.100`). Download it [here](https://www.microsoft.com/net/download/dotnet-core/2.2).
  
  * [Endpoint Routing](/projects/2-2/endpoint-routing)

    Enable Endpoint Routing for your MVC Core app. You will gain a faster performance and more functionalities regarding routing. 

  * [Endpoint Routing - GetUriByAction](/projects/2-2/endpoint-routing-2)

    Use the `LinkGenerator` singleton and its `GetUriByAction` method to generate a link to an Action. It will respect the convention used by MVC, which is, in this example, `app.UseMvcWithDefaultRoute();`.

  * [Endpoint Routing - GetUriByAction - 2](/projects/2-2/endpoint-routing-3)

    Use the `LinkGenerator` singleton and its `GetUriByAction` method to generate a link to an Action. This sample uses various combination of `Route` and `HttpGet` attributes to generate various links.
    
  * [Endpoint Routing - GetUriByAction - 3](/projects/2-2/endpoint-routing-4)

    Show how to deal with route with values using `LinkGenerator.GetUriByAction`. If you don't deal with the values, the link generator won't generate the link.

  * Endpoint Routing - GetTemplateByAction

    __This sample is no longer relevant__. `LinkGenerator.GetTemplateByAction` has been removed from ASP.NET Core 2.2 final version.
    Demonstrate on how to obtain route template from an existing Action using `LinkGenerator.GetTemplateByAction` and generate path using the information.

  * [Endpoint Routing - GetPathByAction](/projects/2-2/endpoint-routing-6)

    Show how to deal with route with values using `LinkGenerator.GetPathByAction`. If you don't deal with the values, the link generator won't generate the link.

  * [Parameter Transformer ](/projects/2-2/parameter-transformer)
     
    Use Parameter Transformer to control the creation of route token `[area]`, `[controller]` and `[action]`. In this example we use it on `[controller]` and `[action]`.
   
  * [Health Check - Check URL](/projects/2-2/health-check)

    Show the simplest way to use health check functionality using `app.UseHealthChecks`.

  * [Health Check - Check URL - 2](/projects/2-2/health-check-2)

    Customize the message returned by `app.UseHealthChecks`.

  * [Health Check - Check URL - 3](/projects/2-2/health-check-3)

    Start implementing `IHealthCheck` to provide status information for the health check service. In this example, it will always return failure because we just throw an exception in the implementation. You will see how the health check handles an unhandled exception.

  * [Health Check - Check URL - 4](/projects/2-2/health-check-4)

    Implement a `IHealthCheck` that check the status of a url. This is the first version of the check so it is primitive but it is also easier to understand. We will go to a more sophisticated multi check in the next examples.

  * [Health Check - Check URL - 5](/projects/2-2/health-check-5)

    Similar to the previous example except that now there are two checks, one fails and one successful. 

  * [Health Check - Check URL - 6](/projects/2-2/health-check-6)

    Similar to the previous example except that one of the check reports "Degraded" status by using `context.Registration.FailureStatus = HealthStatus.Degraded;`.

  * [New Redis Caching Package](/projects/2-2/new-redis-caching-package)

    Instead of using `Microsoft.Extensions.Caching.Redis`, use the new `Microsoft.Extensions.Caching.StackExchangeRedis`. The former will be deprecated in .NET Core 3.0 (https://github.com/aspnet/Announcements/issues/322). 

## What's new in ASP.NET Core 2.1(5)

  *Pre-requisite*: Make sure you download .NET Core SDK [2.1.502](https://dotnet.microsoft.com/download/dotnet-core/2.1#sdk-2.1.502) otherwise below examples won't work.

  **New code based idiom to start your host for ASP.NET Core 2.1**

  It is recommended to use the following approach 

  ```CSharp
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
  ```

  instead of

  ```CSharp
      public class Program
      {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development")
                .Build();
      }
  ```

  * [Hello World with Microsoft.AspNetCore.App package](/projects/hello-world-startup-app-package)

    If you are targeting `netcoreapp2.1`, you can use `Microsoft.AspNetCore.App` meta package that download **most** of the necessary packages to develop an ASP.NET Core/MVC system (including EF DB support).

    This package is a trimmed version of `Microsoft.AspNetCore.All` meta package. You can find more details about the removed dependencies [here](https://github.com/aspnet/Announcements/issues/287).

    `Microsoft.AspNetCore.App` is going to be the default meta package when you create a new ASP.NET Core 2.1 package.

  * [HttpClientFactory](/projects/httpclientfactory)

    Now you can have centrally managed instance of HttpClient using ```IHttpClientFactory``` via dependency injection.

  * [HttpClientFactory - 2](/projects/httpclientfactory-2)

    Use preconfigured `HttpClient` via `IHttpClientFactory`.

  * [HttpClientFactory - 3](/projects/httpclientfactory-3)

    Use `IServiceCollection.AddHttpClient` to provide `HttpClient` for your classes.

  * [HttpClientFactory - 4](/projects/httpclientfactory-4)

    Use `IServiceCollection.AddHttpClient` to provide `HttpClient` for interface-implementing classes.

  * [Supress Status Messages](/projects/suppress-status-messages)

    You can hide status messages when you start up your web application. It's a small useful thing.

## What's new in ASP.NET Core 2.0 (11)

  This section will show new things in [ASP.NET Core 2.0](https://github.com/aspnet/Home/releases/tag/2.0.0). This is a good explanation on [what's new on ASP.NET Core 2.0](https://blogs.msdn.microsoft.com/webdev/2017/08/25/asp-net-core-2-0-features-1/).

  * [Hello World with Microsoft.AspNetCore.All package](/projects/hello-world-startup-all-package)

      If you are targeting `netcoreapp2.0`, you can use `Microsoft.AspNetCore.All` meta package that download **most** of the necessary packages to develop an ASP.NET Core/MVC system (including EF DB support).

      It also adds the following packages

      ```
      Installing Microsoft.IdentityModel.Logging 1.1.4.
      Installing Microsoft.IdentityModel.Tokens 5.1.4.
      Installing runtime.win-x64.runtime.native.System.Data.SqlClient.sni 4.4.0.
      Installing runtime.win-x86.runtime.native.System.Data.SqlClient.sni 4.4.0.
      Installing runtime.win-arm64.runtime.native.System.Data.SqlClient.sni 4.4.0.
      Installing System.IdentityModel.Tokens.Jwt 5.1.4.
      Installing System.Text.Encoding.CodePages 4.4.0.
      Installing runtime.native.System.Data.SqlClient.sni 4.4.0.
      Installing Microsoft.Azure.KeyVault.WebKey 2.0.7.
      Installing Microsoft.Rest.ClientRuntime.Azure 3.3.7.
      Installing Microsoft.Rest.ClientRuntime 2.3.8.
      Installing SQLitePCLRaw.lib.e_sqlite3.v110_xp 1.1.7.
      Installing SQLitePCLRaw.lib.e_sqlite3.linux 1.1.7.
      Installing SQLitePCLRaw.lib.e_sqlite3.osx 1.1.7.
      Installing SQLitePCLRaw.provider.e_sqlite3.netstandard11 1.1.7.
      Installing Microsoft.IdentityModel.Protocols 2.1.4.
      Installing Microsoft.NETCore.App 2.0.0-preview2-25407-01.
      Installing Microsoft.NETCore.DotNetHostPolicy 2.0.0-preview2-25407-01.
      Installing Microsoft.NETCore.Platforms 2.0.0-preview2-25405-01.
      Installing NETStandard.Library 2.0.0-preview2-25401-01.
      Installing Microsoft.NETCore.DotNetHostResolver 2.0.0-preview2-25407-01.
      Installing Microsoft.Packaging.Tools 1.0.0-preview2-25401-01.
      Installing System.Interactive.Async 3.1.1.
      Installing SQLitePCLRaw.core 1.1.7.
      Installing Microsoft.IdentityModel.Protocols.OpenIdConnect 2.1.4.
      Installing SQLitePCLRaw.bundle_green 1.1.7.
      Installing Microsoft.Azure.KeyVault 2.3.2.
      Installing Microsoft.IdentityModel.Clients.ActiveDirectory 3.14.1.
      Installing WindowsAzure.Storage 8.1.4.
      Installing System.Data.SqlClient 4.4.0.
      Installing Microsoft.NETCore.DotNetAppHost 2.0.0-preview2-25407-01.
      ```

      In ASP.NET Core 2.0, this is the recommended way to start your host

      ```
      public class Program
      {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development")
                .Build();
      }
      ```

  * [A new way of configuring logging](/projects/logging)

    Now you configure logging at `Program` instead of `Startup.Configure` via `ConfigureLogging`. 

  * [Logging filtering](/projects/logging-with-filter)

    Now you can adjust what kind of logging information from various part of ASP.NET Core and your app you want show/stored.

  * [IConfiguration is now core](/projects/iconfiguration)

    ASP.NET Core 1.1

    ```
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure
            app.Run(context =>
            {
                return context.Response.WriteAsync('hello world');
            });
        }
    ```

    ASP.NET Core 2.0

    ```
      public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
      {
          //These are the four default services available at Configure
          app.Run(context =>
          {
              return context.Response.WriteAsync(configuration["greeting"]);
          });
      }
    ```

  * [Session Feature with Redis using BinaryFormatter](/projects/features-session-redis)

    This shows how to use session with ```Redis``` store. We use ```BinaryFormatter``` which is only available at `.NET Core 2.0` or above to serialize and deserialize your object. *The better way is to serialize your object using Json - BinaryFormatter is SLOW*.

    Make sure you have ```Redis``` running on your ```localhost``` at default port. The connection string is specified at ```appsetings.json```.

  * [Session Feature with Redis using JSON Serialization](/projects/features-session-redis-2)

    This shows how to use session with ```Redis``` store using Json instead of ```BinaryFormatter```.

    Make sure you have ```Redis``` running on your ```localhost``` at default port. The connection string is specified at ```appsetings.json```.

  * [Anti Forgery on Form](/projects/anti-forgery)

    This exists on since .NET Core 1.0 however the configuration for the cookie has changed slightly. We are using ```IAntiForgery``` interface to store and generate anti forgery token to prevent XSRF/CSRF attacks. 

  * [Razor Pages Basic](/projects/mvc/razor-pages-basic)

    This is the simplest example of the brand new `Razor Pages`. It shows the two approaches to `Razor Pages`, one with inline code behind and another with separate code behind.

  * [Razor Pages and MVC Basic](/projects/mvc/razor-pages-mvc)

    Compare and contrast on how the same task can be performed by using `Razor Pages` and `MVC`.

    This sample also shows you how to us `Entity Framework Core` In-Memory Database.

  * [UseRouter extension](/projects/use-router)

    Use ```app.UseRouter()``` extension to create minimalistic HTTP services similar to Nancy.

  * [UseRouter extension 2](/projects/use-router-2)

    Use ```app.UseRouter()``` with alternative lambda signature.

  * [IHostedService](/projects/ihosted-service)

    Implement background tasks using the new `IHostedService` interface.

## Foundation ASP.NET Core 2.1 Samples

All these projects require the following dependencies

```
   "Microsoft.AspNetCore" : "2.1.0"
```


* **Hello World (22)**

  * [Hello World with reload](/projects/hello-world-with-reload)

    Setup your most basic web app and enable the change+refresh development experience. 

    We are using ```IApplicationBuilder Run```, an extension method for adding terminal middleware.

  * Startup class
    * [Hello World with startup basic](/projects/hello-world-startup-basic)

      This project contains all the available services available in Startup class constructor, `ConfigureServices` and `Configure` methods.

    * [Hello World with custom startup class name](/projects/hello-world-startup-custom-name)

      You don't have to call your startup class `Startup`. Any valid C# class will do.

    * [Hello World with responding to multiple urls](/projects/hello-world-startup-basic-multiple-urls)

      Configure so that your web app responds to multiple urls.

    * [Hello World with multiple startups](/projects/hello-world-startup-basic-multiple)

      This project highlights the fact that you can create multiple Startup classes and choose them at start depending on your needs. 

    * [Hello World with multiple startups using environment](/projects/hello-world-startup-basic-multiple-environment)

      This project highlights the fact that you can create multiple startup classes and choose them at start depending on your needs depending on your environment (You do have to name the startup class with Startup). 

    * [Hello World with multiple Configure methods based on environment](/projects/hello-world-startup-multiple-configure-environment)

      This project demonstrates the ability to pick `Configure` method in a single Startup class based on environment.

    * [Hello World with multiple ConfigureServices methods based on environment](/projects/hello-world-startup-multiple-configure-environment-services)

      This project demonstrates the ability to pick `ConfigureServices` method in a single Startup class based on environment.

    * [Hello World with an implementation of IStartup interface](/projects/hello-world-startup-istartup)

      We are really getting into the weed of startup right now. This is an example on how to implement `IStartup` directly. 

    * [Hello World without a startup class](/projects/hello-world-no-startup)

      Why? just because we can.

    * [Hello world with IStartupFilter](/projects/hello-world-startup-istartupfilter)

      Use `IStartupFilter` to configure your middleware. This is an advanced topic. [This article](https://andrewlock.net/exploring-istartupfilter-in-asp-net-core/) tries at explaining `IStartupFilter`. I will add more samples so `IStartupFilter` can be clearer.

  * [Show errors during startup](/projects/hello-world-startup-capture-errors)

    Show a detailed report on exceptions that happen during the startup phase of your web app. It is very useful during development.

  * [Show Connection info](/projects/hello-world-with-connection-info)

    Enumerate the connection information of a HTTP request.

  * [Environmental settings](/projects/hello-world-env-development)

    Set your application environment to `Development` or `Production` or other mode directly from code. 

  * [Console logging](/projects/hello-world-with-logging)

    Setup a basic logging in your app and show it to console.

    We add the following dependencies ```"Microsoft.Extensions.Logging": "1.1.0"``` and ```"Microsoft.Extensions.Logging.Console": "1.1.0"```

    We are using ```IApplicationBuilder Run```, an extension method for adding terminal middleware.

  * [Console logging - without framework log ](/projects/hello-world-with-logging-filtered)

    Filter out frameworking logging from your log output. Without filtering, logging can get very annoying because the framework produces a lot of messages.

  * [Hello World with middlewares](/projects/hello-world-with-middleware)

    ASPNetCore is built on top of pipelines of functions called middleware. 
    
    We are using ```IApplicationBuilder Use```, an extension method for adding middleware and ```IApplicationBuilder Run```.

  * [IApplicationLifetime](/projects/hello-world-with-IApplicationLifetime)

    Respond to application startup and shutdown.

    We are using ```IApplicationLifetime``` that trigger events during application startup and shutdown.

  * [IHostingEnvironment](/projects/hello-world-with-IHostingEnvironment)

    `IHostingEnvironment` is available at `Startup` constuctor and `Startup.Configure`. This sample shows all the properties available in this interface. 

  * [IHostingEnvironment at ConfigureServices](/projects/hello-world-with-IHostingEnvironment-Configure-Services)

    This sample shows how to access `IHostingEnvironment` from `ConfigureServices`. 

  * [Application Environment](/projects/hello-world-application-environment)

    This sample shows how to obtain application environment information (target framework, etc).

  * [Adding HTTP Response Header](/projects/hello-world-with-header)

    Demonstrate on how to add a response header and where is allowed place to do it.

* **Request(8)**
  
  This section shows all the different ways you capture input and examine request to your web application.

  * **HTTP Verb (1)**
    * [Get request verb](/projects/hello-world-startup-basic-request-verb)
      
      Detect the verb/method of the current request. 

  * **Headers (2)**
    * [Access Request Headers](/projects/hello-world-startup-basic-request-headers)
      
      Enumerate all the available headers in a request.

    * [Type Safe Access to Request Headers](/projects/hello-world-startup-basic-request-headers-typed)
      
      Instead of using string to access HTTP headers, use type safe object properties to access common HTTP headers.

  * **Query String (1)**
    * [Single value query string](/projects/hello-world-startup-basic-request-query-string)

      Access single value query string.

    * [Multiple values query string](/projects/hello-world-startup-basic-request-query-string-2)

      Access multiples values query string.

    * [List all query string values](/projects/hello-world-startup-basic-request-query-string-3)

      List all query string values. Also shows the implicat conversion from ```StringValues``` to ```string```.


  * **Form (2)**

    * [Form Values](/projects/form-values) 
      
      Handles the values submitted via a form.

    * [Form Upload File](/projects/form-upload-file) 
      
      Upload a single file and save it to the current directory (check out the usage of ```.UseContentRoot(Directory.GetCurrentDirectory())```)

  * **Cookies (2)**
          
      * [Cookies](/projects/hello-world-with-cookies)

        Read and write cookies.

      * [Removing cookies](/projects/hello-world-with-cookies-2)

        Simply demonstrates on how to remove cookies.

* **Routing (9)**

  We go deep into `Microsoft.AspNetCore.Routing` library that provides routing facilities in your aspnetcore apps.
  There are several samples to illuminate this powerful library.

  * [Router](/projects/routing)
  
    A single route handler that handles every path request.

  * [Router 2](/projects/routing-2)
  
    Two route handler, one for home page (/) and the other takes the rest of the request using asterisk (*) in the url template.

  * [Router 3](/projects/routing-3)

    We are exploring default handler - this is the entry point to create your own framework.
    
  * [Router 4](/projects/routing-4)

    We are mixing optional route parameter, route parameter with default value and default handler.

  * [Router 5]

    This is still broken. I am trying to figure out how to do nested routing. Wish me luck!
  
  * [Router 6](/projects/routing-6)

    We are building a template route segment by segment and parts by parts, oldskool. We are using ```TemplateMatcher```, ```TemplateSegment``` and ```TemplatePart```.

    Hold your mask, we are going deep.
  
  * [Router 7](/projects/routing-7)

    We are creating a routing template with two segments, one with Literal part and the other Parameter part, e.g, "/page/{*title}"

  * [Router 8](/projects/routing-8)

    We are creating a routing template with one segment consisted of two parts, one Literal and one Parameter, e.g. "/page{*title}". Note the difference between this example and Router 7.

  * [Router 9](/projects/routing-9)
   
    I am still trying to determine whether `TemplateMatcher` uses the `InlineConstraint` information.

    Update: No, `TemplateMatcher` does not run constraints. [#362](https://github.com/aspnet/Routing/issues/362)
 
  * [Router 10](/projects/routing-10)    

    We have been building a `RouteTemplate` manually using `TemplateSegment` and `TemplatePart`. In this example we are using `TemplateParser` to build the `RouteTemplate` using string.

* **Middleware (10)**

  We will explore all aspect of middleware building in this section. There is no extra dependency taken other than `Kestrel` and `dotnet watch`. 

  * [Middleware 1](/projects/middleware-1)
   
    This example shows how to pass information from one middleware to another using `HttpContext.Items`.

  * [Middleware 3](/projects/middleware-3)
   
    This is the simplest middleware class you can create. 

  * [Middleware 4](/projects/middleware-4)
   
    Use `app.Map` (`MapMiddleware`) to configure your middleware pipeline to respond only on specific url path.

  * [Middleware 5](/projects/middleware-5)
   
    Nested `app.Map` (show `Request.Path` and `Request.PathBase`).

  * [Middleware 6](/projects/middleware-6)
   
    Use `app.MapWhen`(`MapWhenMiddleware`) and Nested `app.Map` (show `Request.Path` and `Request.PathBase`).

  * [Middleware 7](/projects/middleware-7)
   
    Use `MapMiddleware` and `MapWhenMiddleware` directly without using extensions (show `Request.Path` and `Request.PathBase`).

  * [Middleware 8](/projects/middleware-8)
   
    Demonstrate the various ways you can inject dependency to your middleware class *manually*. 

  * [Middleware 9](/projects/middleware-9)
   
    Demonstrate how to ASP.NET Core built in DI (Dependency Injection) mechanism to provide dependency for your middleware.

  * [Middleware 10](/projects/middleware-10)
   
    Demonstrate that a middleware is a singleton.

  * [Middleware 11](/projects/middleware-11)
   
    This sample is similar to `Middleware 10` except that this one implement `IMiddleware` to create Factory-based middleware activation. This means that you can create a middleware that is not a singleton (either Transient or Scoped). 


* **Features (8)**
  
  Features are collection of objects you can obtain from the framework at runtime that serve different purposes.

  * [Server Addresses Feature](/projects/features-server-addresses)

    Use this Feature to obtain a list of urls that your app is responding to.

  * [Server Addresses Feature - 2](/projects/features-server-addresses-2)

    Use `IServer` interface to access server addressess when you don't have access to `IApplicationBuilder`. 

  * [Request Feature](/projects/features-server-request)

    Obtain details of a current request. It has some similarity to HttpContext.Request. They are not equal. `HttpContext.Request` has more properties.  

  * [Connection Feature](/projects/features-connection)

    Use `IHttpConnectionFeature` interface to obtain local ip/port and remote ip/port. 

  * [Custom Feature](/projects/features-server-custom)

    Create your own custom Feature and pass it along from a middleware. 

  * [Custom Feature - Override](/projects/features-server-custom-override)

    Shows how you can replace an implementation of a Feature with another within the request pipeline.

  * [Request Culture Feature](/projects/features-request-culture)

    Use this feature to detect the culture of a web request through `IRequestCultureFeature`. It needs the following dependency `"Microsoft.AspNetCore.Localization": "2.1.0"`.

  * [Session Feature](/projects/features-session)

    Use session within your middlewares. This sample shows a basic usage of in memory session. It needs the following dependency '"Microsoft.AspNetCore.Session" : "1.1.0-*"` and `"Microsoft.Extensions.Caching.Memory" : "2.1.0-*"`.

* **Dependency Injection (2)**

  ASP.NET Corenetcore lives and die by DI. It relies on `Microsoft.Extensions.DependencyInjection` library. There is no need to put this dependency in your `project.json` explicitly because aspnetcore already has this package as its own dependency.

  * [Dependency Injection 1 - The basic](/projects/dependency-injection-1)

    Demonstrate the three lifetime registrations for the out of the box DI functionality: singleton (one and only forever), scoped (one in every request) and transient (new everytime).

  * [Dependency Injection 3 - Easy registration](/projects/dependency-injection-3)
  
    Register all objects configured by classes that implements a specific interface (`IBootstrap` in this example). This is useful when you have large amount of classes in your project that needs registration. You can register them near where they are (usually in the same folder) instead of registering them somewhere in a giant registration function.

    Note: example 2 is forthcoming. The inspiration has not arrived yet.

* **File Provider (2)**
  
  We will deal with various types of file providers supported by ASP.NET Core

  * [Physical File Provider - Content and Web roots](/projects/file-provider-physical)

    Access the file information on your Web and Content roots. 

  * [Custom File Provider](/projects/file-provider-custom)

    Implement a simple and largely nonsense file provider. It is a good starting point to implement your own proper File Provider.
    

* **In Memory Caching (a.k.a local cache) (4)**

  These samples depends on `Microsoft.Extensions.Caching.Memory` library. Please add this dependency to your `project.json`.

  * [Caching - Absolute/Sliding expiration](/projects/caching)

    This is the most basic caching you can use either by setting absolute or sliding expiration for your cache. Absolute expiration will remove your cache at a certain point in the future. Sliding expiration will remove your cache after period of inactivity.

  * [Caching 2 - File dependency](/projects/caching-2)
    
    Add file dependency to your caching so when the file changes, your cache expires.

    You need to put the cache file in your `project.json` so it gets copied over, e.g.

    `"buildOptions": {
        "emitEntryPoint": true,
        "copyToOutput": ["cache-file.txt"]
    }`

    Note: example 1 is forthcoming. The inspiration has not arrived yet.

  * [Caching 3 - Cache removal event](/projects/caching-3)

    Register callback when a cached value is removed.

  * [Caching 4 - CancellationChangeToken dependency](/projects/caching-4)

    Bind several cache entries to a single dependency that you can reset manually.

* **Configuration (7)**

  This section is all about configuration, from memory configuration to INI, JSON and XML.

  * [Configuration](/projects/configuration)

    This is the 'hello world' of configuration. Just use a memory based configuration and read/write values to/from it.

  * [Configuration - Options](/projects/configuration-options)

    Use IOptions at the most basic.

  * [Configuration - Environment variables](/projects/configuration-environment-variables)

    Load environment variables and display all of them.

  * [Configuration - INI file](/projects/configuration-ini)

    Read from INI file. It requires taking a new dependency, `"Microsoft.Extensions.Configuration.INI" : "2.1.0"`.

  * [Configuration - INI file - Options](/projects/configuration-ini-options)

    Read from INI file (with nested keys) and IOptions. It requires taking two new dependencies, `"Microsoft.Extensions.Configuration.INI" : "2.1.0"` and `"Microsoft.Extensions.Options.ConfigurationExtensions" : "2.1.0"`.

  * [Configuration - XML file](/projects/configuration-xml)

    Read from XML file. It requires taking a new dependency, `"Microsoft.Extensions.Configuration.Xml" : "2.1.0"`.

    **Note**: This Xml Configuration provider does not support repeated element.

    The following configuration settings will break:

    ```
    <appSettings>
      <add key="webpages:Version" value="3.0.0.0" />
      <add key="webpages:Enabled" value="false" />
    </appSettings>
    ```

    On the other hand you can get unlimited nested elements and also attributes.

  * [Configuration - XML file - Options](/projects/configuration-xml-options)

    Read from XML file and use IOptions. It requires taking two new dependencies, `"Microsoft.Extensions.Configuration.Xml" : "2.1.0"` and `"Microsoft.Extensions.Options.ConfigurationExtensions" : "2.1.0"`.

* **Localization and Globalization (6)**

  This section is all about languages, culture, etc.

  * [Localization](/projects/localization)

    Shows the most basic use of localization using a resource file. This sample only supports French language (because we are fancy). It needs the following dependency `"Microsoft.AspNetCore.Localization": "2.1.0"` and  `"Microsoft.Extensions.Localization": "2.1.0"`.

  * [Localization - 2](/projects/localization-2)

    We build upon the previous sample and demonstrate how to switch request culture via query string using the built in `QueryStringRequestCultureProvider`. This sample supports English and French.

  * [Localization - 3](/projects/localization-3)

    Demonstrate the difference between `Culture` and `UI Culture`.

  * [Localization - 4](/projects/localization-4)

    Demonstrate how to switch request culture via cookie using the built in `CookieRequestCultureProvider`. This sample supports English and French.

  * [Localization - 5](/projects/localization-5)

    Demonstrate using Portable Object (PO) files to support localization instead of the cumbersome resx file. This sample requires ```OrchardCore.Localization.Core``` package. This sample requires ```ASPNET Core 2```.

  * [Localization - 6](/projects/localization-6)

    This is a continuation of previous sample but with context, which allows the same translation key to return different strings.

* **URL Redirect/Rewriting (6)**
  
  This section explore the dark arts of URL Rewriting

  * [Rewrite](/projects/rewrite)
    
    Shows the most basic of URL rewriting which will **redirect** (returns [HTTP 302](https://en.wikipedia.org/wiki/HTTP_302)) anything to the home page "/". It requires an additional `"Microsoft.AspNetCore.Rewrite" : "2.1.0-*"` dependency. 
    
    If you have used routing yet, I recommend of checking out the routing examples.

  * [Rewrite - 2](/projects/rewrite-2)
    
    **Redirect** (returns [HTTP 302](https://en.wikipedia.org/wiki/HTTP_302)) anything with an extension e.g. about-us.html or welcome.aspx to home page (/). It also shows how to capture the matched regex values.

  * [Rewrite - 3](/projects/rewrite-3)

    **Rewrite** anything with an extension e.g. about-us.html or welcome.aspx to home page (/). It also shows how to capture the matched regex values.
    
  * [Rewrite - 4](/projects/rewrite-4)
    
    **Permanent Redirect** (returns [HTTP 301](https://en.wikipedia.org/wiki/HTTP_301)) anything with an extension e.g. about-us.html or welcome.aspx to home page (/). It also shows how to capture the matched regex values.

  * [Rewrite - 5](/projects/rewrite-5)
  
    Implement a custom redirect logic based on `IRule` implementation. Require additional dependency of `"Microsoft.AspNetCore.StaticFiles": "1.1.0"` to serve images.

    This custom redirection logic allows us to simply specify the image file names without worrying about their exact path e.g.'xx.jpg' and 'yy.png'.

  * [Rewrite - 6](/projects/rewrite-6)
  
    Implement a custom redirect logic using lambda (similar functionality to Rewrite - 5). Require additional dependency of `"Microsoft.AspNetCore.StaticFiles": "1.1.0"` to serve images.

    This custom redirection logic allows us to simply specify the image file names without worrying about their exact path e.g.'xx.jpg' and 'yy.png'.

* **Compression (1)**

  Enable the ability to compress ASP.NET Core responses. These samples takes a dependency of ```Microsoft.AspNetCore.ResponseCompression": "2.1.0```.

  * [Default Gzip Output Compression](/projects/compression-response) 
   
    Compress everything using the default Gzip compression.

    _Everything_ means the following MIME output  
    
    * text/plain
    * text/css
    * application/javascript
    * text/html
    * application/xml
    * text/xml
    * application/json
    * text/json 

* **Diagnostics(6)**

  These samples take a dependency of ```"Microsoft.AspNetCore.Diagnostics":"1.1.1"```. 

  * [Welcome Page](/projects/diagnostics)

    Simply show a welcome page to indicate that the app is working properly. This sample does not use a startup class simply because it's just a one line code.

  * [Developer Exception Page](/projects/diagnostics-2)

    Show any unhandled exception in a nicely formatted page with error details. Only use this in development environment!

  * [Custom Global Exception Page](/projects/diagnostics-3)

    Use ```IExceptionHandlerFeature``` feature provided by ```Microsoft.AspNetCore.Diagnostics.Abstractions``` to create custom global exception page.

  * [Custom Global Exception Page - 2](/projects/diagnostics-4)

    Similar to the previous one except that that we use the custom error page defined in separate path.

  * [Status Pages](/projects/diagnostics-5)

    Use ```UseStatusCodePagesWithRedirects```.  **Beware:** This extension method handles your 5xx return status code by redirecting it to a specific url. It will not handle your application exception in general (for this use ```UseExceptionHandler``` - check previous samples).

  * [Middleware Analysis](/projects/diagnostics-6)

    Here we go into the weeds of analysing middlewares in your request pipeline. This is a bit complicated. It requires the following packages:

    * ```Microsoft.AspNetCore.MiddlewareAnalysis```
    * ```Microsoft.Extensions.DiagnosticAdapter```
    * ```Microsoft.Extensions.Logging.Console```

* **Static Files(6)**

    This additional dependency is required to enable the functionality ```"Microsoft.AspNetCore.StaticFiles": "1.1.0"```. 

  * [Serve static files](/projects/serve-static-files)

    Simply serve static files (html, css, images, etc).     
    
    There are two static files being served in this project, index.html and hello.css. They are stored under ```wwwroot``` folder, which is the default folder location for this library. 
    
    To access them you have to refer them directly e.g. ```localhost:5000/index.html``` and ```localhost:5000/hello.css```.

  * [Allow Directory Browsing](/projects/serve-static-files-2)
    
    Allow listing and browsing of your ```wwwroot``` folder.

  * [Use File Server](/projects/serve-static-files-3)
    
    Combines the functionality of ```UseStaticFiles, UseDefaultFiles, and UseDirectoryBrowser```.

  * [Custom Directory Formatter](/projects/serve-static-files-4)
    
    Customize the way Directory Browsing is displayed. In this sample the custom view only handles flat directory. We will deal with 
    more complex scenario in the next sample.

  * [Custom Directory Formatter - 2](/projects/serve-static-files-5)
    
    Show custom Directory Browsing and handle directory listing as well as files.

  * [Allow Directory Browsing](/projects/serve-static-files-6)
    
    Use Directory Browsing on a certain path using ```DirectoryBrowserOptions.RequestPath```, e.g. ```/browse```.

* **Web Sockets (5)**

  We are going to explore websocket functionality provided by ASP.NET Core. All the samples here require ```Microsoft.AspNetCore.WebSockets```. 

  **Warning**: These samples are low level websocket code. For production, use [SignalR](https://github.com/aspnet/signalr). Yes I will work on SignalR samples soon.

  * [Echo Server](/projects/web-sockets)

    This is the simplest web socket code you can write. It simply returns what you sent. It does not handle the closing of the connection. It does not handle data that is larger than buffer. It only handles text payload.

  * [Echo Server 2](/projects/web-sockets-2)

    We improve upon the previous sample by adding console logging (requiring ```Microsoft.Extensions.Logging.Console``` package) and handling data larger than the buffer. I set the buffer to be very small (4 bytes) so you can see how it works.

  * [Echo Server 3](/projects/web-sockets-3)
  
    We improve upon the previous sample by enabling broadcast. What you see here is a very crude chat functionality.

  * [Echo Server 4](/projects/web-sockets-4)

    We improve upon the previous sample by handling closing event intiated by the web client.
    
  * [Chat Server](/projects/web-sockets-5)

    Implement a rudimentary single channel chat server.

* **Server Side Events (1)**

  * [Forever Server](/projects/sse)

    This server will send a 'hello world' greeting forever.

* **Syndications (2)**

  We are using the brand new ```Microsoft.SyndicationFeed.ReaderWriter``` package to read RSS and ATOM feeds.

  * [Syndication - Read RSS](/projects/syndication)

    This is the shortest amount of code to read an RSS feed. This example read the feed from the inventor of RSS, Dave Winer at http://scripting.com/rss.xml. 
  
  * [Syndication - Read RSS with extensions](/projects/syndication-2)

    This sample process RSS Outline Extension. 

* **Misc (3)**

  * [Markdown server](/projects/markdown-server)

    Serve markdown file as html file. You will see how you can create useful app using a few basic facilities in aspnetcore.

    We take ```"Markdig" : "0.15.1"``` as dependency. 
    
  * [Markdown server - implemented as middleware component](/projects/markdown-server-middleware)

    Serve markdown file as html file. It has the same exact functionality as [Markdown server](/projects/markdown-server) but implemented using middleware component.

    We take ```"Markdig" : "0.15.1"``` as dependency. 

    [Check out](https://docs.asp.net/en/latest/migration/http-modules.html) the documentation on how to write your own middleware.

  * [Password Hasher server](/projects/password-hasher)

    Give it a string and it will generate a secure hash for you, e.g. ```localhost:5000?password=mypassword```.

    We add dependency ```"Microsoft.AspNetCore.Identity": "2.1.0"``` to enable this functionality.

* **Web Utilities(2)**

  This section shows various functions avaiable at `Microsoft.AspNetCore.WebUtilities`. 

  * [Query Helpers](/projects/web-utilities-query-helpers)

    This utility helps you generate query string for your url safely (ht [Rehan Saeed](https://rehansaeed.com/asp-net-core-hidden-gem-queryhelpers/)).

  * [Reason Phrases](/projects/web-utilities-reason-phrases)

    This utility returns HTTP reasonse phrases given a status code number.
    

* **Uri Helper(1)**
  
  This section shows various methods available at `Microsoft.AspNetCore.Http.Extensions.UriHelper`.

  * [Get Display Url](/projects/uri-helper-get-display-url) 

    `Request.GetDisplayUrl()` shows complete url with host, path and query string of the current request. It's to be used for display purposes only.


* **Trimming (1)**
  
  This section shows the various way on how to trim the size of your application by using [Microsoft.Packagin.Tools.Trimming](https://www.nuget.org/packages/Microsoft.Packaging.Tools.Trimming/1.1.0-preview1-26619-01)

  * [Trimming Microsoft.AspNetCore.All hello world application](/projects/hello-world-startup-all-package-trimming)

    Run ```dotnet publish``` or ```dotnet build``` and read the output in your terminal. It will read something similar to ```Trimmed 115 out of 168 files for a savings of 18.93 MB Final app size is 3.07 MB```. You can turn off the trimming by setting ```<TrimUnusedDependencies>true</TrimUnusedDependencies>``` to ```false``` at the project file.

* **Email (1)**

  This section shows samples of using [MailKit](https://github.com/jstedfast/MailKit), which is essentially **the** library to use for sending and receiving email in ASP.NET Core.

  * [Send email](/projects/mailkit)
  
    This shows an example on how to send an email using SMTP.


* **Modules (2)**

  This section shows how to create pluggable and extensible web system using ```Orchardcore Modules``` system.

  * [Modular Hello World](/projects/orchardcore-module)

    Run ```dotnet watch run``` at the ```web``` folder. This example shows a module that just writes "hello world".

    The ```module1``` project requires ```OrchardCore.Module.Targets``` and the host ```web``` project requires ```OrchardCore.Application.Targets``` and ```OrchardCore.Modules```.


  * [Keeping track of anonymous users](/projects/anonymous-id)

    Keep track of anonymous user in your ASP.NET Core (useful in scenario such as keeping track of shopping cart) using `ReturnTrue.AspNetCore.Identity.Anonymous` library.


* **Middleware (1)**
  
  * [Response Buffering](/projects/response-buffering)

    Use `Microsoft.AspNetCore.Buffering 0.2.2` middleware to implement response buffering facility. This will allow you to change your response after you write them.

* **Device Detection (1)**
  
  The samples in this section rely on [Wangkanai.Detection](https://github.com/wangkanai/Detection) library.

  * [Device Detection](/projects/device-detection)

    This is the most basic device detection. You will be able to detect whether the client is a desktop or a mobile client.


* **Owin (1)**

  All these samples require ```Microsoft.AspNetCore.Owin``` package. These are low level samples and in most cases are not relevant to your day to day ASP.NET Core development.

  * [Owin](/projects/owin)

    Hello world the hard way.

* **Image Sharp (1)**

  All these samples require `SixLabors.ImageSharp.Web` middleware package. This middleware is an excelent tool to process your day to day image processing need.

  * [Image-Sharp](/projects/image-sharp)

    This example shows how to enable image resizing functionality to your site. It's super easy and the middleware takes care of caching, etc.
  
## Generic Host (9)

  Generic Host is an awesome way to host all sort of long running tasks and applications, e.g. messaging, background tasks, etc.

  This section is dedicated to all the nitty gritty of Generic Host. All the examples in this section rely on `Microsoft.AspNetCore.App` package.

  * [Hello World](/projects/generic-host)

    This is the hello world equivalent of a Generic Host service.

  * [Hello World using Console Lifetime](/projects/generic-host-2)

    Use `UseConsoleLifetime` implicitly. 

  * [Startup and Shutdown order](/projects/generic-host-3)

    Demonstrates the startup and shutdown order of hosted services.

  * [Start and stop the host](/projects/generic-host-4)

    Demonstrates starting and stopping the host programmatically.

  * [A service with timed execution](/projects/generic-host-5)

    Demonstrate processing a task on a regular interval using `Task.Delay`.

  * [Configure Host using Dictionary](/projects/generic-host-configure-host)

    Demonstrate the way to inject configuration values to the host using Dictionary.

  * [Configure Environment](/projects/generic-host-environment)

    Set your environment using `EnvironmentName.Development` or `EnvironmentName.Production` or `EnvironmentName.Staging`.

  * [Configure Logging](/projects/generic-host-configure-logging)

    Configure logging for your Generic Host.

  * [Listen to IApplicationLifetime events](/projects/generic-host-iapplicationlifetime)

    Inject `IApplicationLifetime` and listen to `ApplicationStarted`, `ApplicationStopping` and `ApplicationStopped` events. This is important to allow services to be shutdown gracefully. The shutdown process blocks until `ApplicatinStopping` and `ApplicationStopped` events complete.

## Other resources

These are other aspnetcore resources with code samples

* [aspnetcore documentation](https://github.com/aspnet/Docs/tree/master/aspnet/)
* [aspnetcore entropy](https://github.com/aspnet/entropy)


## Misc

* [Contributor Guidelines](https://github.com/dodyg/practical-aspnetcore/blob/master/CONTRIBUTING.md)
* [Code of Conduct](https://github.com/dodyg/practical-aspnetcore/blob/master/CODE_OF_CONDUCT.md)

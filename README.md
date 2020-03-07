# Samples for ASP.NET Core 3.1 (WIP)

This branch in a work in progress. I am converting over 200 samples from previous versions to 3.1. If you are starting on ASP.NET Core, you can start from the samples from the master branch. 

## Additional Sections

*Continue to scroll down to find materials for absolute beginners to ASP.NET Core.*

| Sections |
| --------------------------------------------------------------- |
| [ASP.NET Core 3.0](/projects/3-0) |
| [Blazor Client Side (Web Assembly)](/projects/blazor/README.md) |
| [Blazor Server Side](/projects/blazor-ss) |
| [Caching](/projects/caching) |
| [Configurations](/projects/configurations) |
| [Diagnostics](/projects/diagnostics)|
| [Endpoint Routing](/projects/endpoint-routing) |
| [Features](/projects/features) |
| [Health Check](/projects/health-check)|
| [IHttpClientFactory](/projects/httpclientfactory)|
| [Generic Hosting](/projects/generic-host) |
| [gRPC](/projects/grpc)|
| [Middleware](/projects/middleware)|
| [MVC](/projects/mvc)|
| [Razor Pages](/projects/razor-pages)|
| [Static Files and File Provider](/projects/file-provider)|
| [System.Text.Json](/projects/json) |
| [URL Redirect/Rewrite](/projects/rewrite)|
| [Uri Helper](/projects/uri-helper)|
| [Syndications](/projects/syndications)|
| [Web Sockets](/projects/web-sockets)|
| [Web Utilities](/projects/web-utilities)|


<!-- 
| [ASP.NET Core MVC](/projects/mvc/README.md) | 47 | 2.1 |
| [ASP.NET Core Razor Pages](/projects/razor-pages/README.md) | 4|  2.2 |
| [ASP.NET Core SignalR](/projects/signalr/README.md) |1| 2.1 |
| [Security related samples](/projects/security) | 1 |2.2 |
| [Orchard Core Framework](/projects/orchard-core) | 4| 3.0 |
| [What's new in ASP.NET Core 2.2](/projects/2-2) | 14 | 2.2 |
| [What's new in ASP.NET Core 2.1](/projects/2-1) | 6 | 2.1 |
| [What's new in ASP.NET Core 2.0](/projects/2-0) | 11 | Features introduced in 2.0 but samples run on 2.1 |
| [Foundational ASP.NET Core 2.1 Samples](#foundation-aspnet-core-21-samples) | 136 | 2.1 |
-->

## How to run these samples

To run these samples, simply open your command line console,  go to each folder and execute `dotnet watch run`.

## Foundation ASP.NET Core 3.1 Samples

### Basic

* [Hello World](/projects/basic/hello-world)

  This is the simplest ASP.NET Core application you can create. An ASP.NET Core application includes a super fast web server called Kestrel. In a few lines of code we set up the web server and a simple app.

  In this sample we use a `Startup` class to configure your application. This is the canonical way of doing thing.
  
* [Hello World - 2](/projects/basic/hello-world-2)

  This is the equivalent of the previous Hello World sample except that in this case we don't use a `Startup` class. This way of configuring things is not common.  


### Server-Sent Events (1)

* [Forever Server](/projects/sse)

  This server will send a 'hello world' greeting forever.

* **Utils(3)**

  * [Status Codes](/projects/http-status-codes)

    Here we contrast between the usage of `Microsoft.AspNetCore.Http.StatusCodes` and `System.Net.HttpStatusCode`.

  * [MediaTypeNames](/projects/media-type-names)

    This class provides convenient constants for some common MIME types. It's not extensive by any means however `MediaTypeNames.Text.Html` and `MediaTypeNames.Application.Json` come handy.  

  * [MediaTypeNames - 2](/projects/media-type-names-2)

    Using `FileExtensionContentTypeProvider` to obtain the correct MIME type of a filename extension.

<!-- 


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

* **Request(9)**
  
  This section shows all the different ways you capture input and examine request to your web application.

  * **HTTP Verb (1)**
    * [Get request verb](/projects/hello-world-startup-basic-request-verb)
      
      Detect the verb/method of the current request. 

  * **Headers (3)**
    * [Access Request Headers](/projects/hello-world-startup-basic-request-headers)
      
      Enumerate all the available headers in a request.

    * [Access Request Headers using common HTTP header names contained in HeaderNames](/projects/hello-world-startup-basic-request-headers-names)

      This sample shows all the common HTTP header names contained in `HeaderNames` class. So instead of using string to obtain a HTTP Header, you can just use a convenient constant such as `HeaderNames.ContentType`.

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


* **Dependency Injection (2)**

  ASP.NET Corenetcore lives and die by DI. It relies on `Microsoft.Extensions.DependencyInjection` library. 

  * [Dependency Injection 1 - The basic](/projects/dependency-injection-1)

    Demonstrate the three lifetime registrations for the out of the box DI functionality: singleton (one and only forever), scoped (one in every request) and transient (new everytime).

  * [Dependency Injection 3 - Easy registration](/projects/dependency-injection-3)
  
    Register all objects configured by classes that implements a specific interface (`IBootstrap` in this example). This is useful when you have large amount of classes in your project that needs registration. You can register them near where they are (usually in the same folder) instead of registering them somewhere in a giant registration function.

    Note: example 2 is forthcoming. The inspiration has not arrived yet.



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

* **Trimming (1)**
  
  This section shows the various way on how to trim the size of your application by using [Microsoft.Packagin.Tools.Trimming](https://www.nuget.org/packages/Microsoft.Packaging.Tools.Trimming/1.1.0-preview1-26619-01)

  * [Trimming Microsoft.AspNetCore.All hello world application](/projects/hello-world-startup-all-package-trimming)

    Run ```dotnet publish``` or ```dotnet build``` and read the output in your terminal. It will read something similar to ```Trimmed 115 out of 168 files for a savings of 18.93 MB Final app size is 3.07 MB```. You can turn off the trimming by setting ```<TrimUnusedDependencies>true</TrimUnusedDependencies>``` to ```false``` at the project file.

* **Email (1)**

  This section shows samples of using [MailKit](https://github.com/jstedfast/MailKit), which is essentially **the** library to use for sending and receiving email in ASP.NET Core.

  * [Send email](/projects/mailkit)
  
    This shows an example on how to send an email using SMTP.

    Thanks to [@Kinani95](https://twitter.com/Kinani95).

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
  

## Other resources

These are other aspnetcore resources with code samples

* [aspnetcore documentation](https://github.com/aspnet/Docs/tree/master/aspnet/)
* [aspnetcore entropy](https://github.com/aspnet/entropy)
* [aspnetcore API browser](https://docs.microsoft.com/en-us/dotnet/api/?view=aspnetcore-2.2)


## Misc

* [Contributor Guidelines](https://github.com/dodyg/practical-aspnetcore/blob/master/CONTRIBUTING.md)
* [Code of Conduct](https://github.com/dodyg/practical-aspnetcore/blob/master/CODE_OF_CONDUCT.md)
-->
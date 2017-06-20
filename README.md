# 91 samples for aspnetcore fundamentals (updated daily)

## Welcome

The goal of this project is to enable .NET programmers to learn the new ASP.NET Core stack from the ground up directly from code. I will not address ASP.NET Core MVC in this project. There is so much power in the underlying ASP.NET Core stack. Don't miss them! 

You will need to download the **latest release version** [.NET Core SDK](https://www.microsoft.com/net/download/core#/sdk) to be able to run these samples.
 
If you are running **these samples on Linux**, change the target framework inside the csproj files from

```
<TargetFramework>net461</TargetFramework>
```
to
```
<TargetFramework>netcoreapp1.1</TargetFramework>
```

Every sample is designed specifically to demonstrate a single idea. We will go wide and deep to the nitty gritty of ASP.NET Core stack. Enjoy the ride!

Some of the samples you see here involve mixed projects (net461) that will run only in Windows. For many .NET developers, full framework is the reality for forseeable future. We are not going to port multi-year production systems to run on Linux. We want to improve the creaky .NET MVC 2.0 that we have lying around and bring it up to speed to aspnetcore MVC.

All these projects require the following dependencies

```
   "Microsoft.AspNetCore.Hosting" : "1.1.0-*"
```

If a sample require additional dependencies, I will list them.

I highly recommend using [Visual Studio Code](https://code.visualstudio.com/) to play around with these samples but it is not required. You can use Visual Studio 2017 as well.

## How to run these samples

To run these samples, simply open your command line console,  go to each folder and execute ```dotnet restore``` and then continue with ```dotnet watch run```.


## List
* **Hello World (21)**

  * [Hello World with reload](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-reload)

    Setup your most basic web app and enable the change+refresh development experience. 
    
    We are using ```IApplicationBuilder Run```, an extension method for adding terminal middleware.

  * Startup class
    * [Hello World with startup basic](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-startup-basic)

      This project contains all the available services available in Startup class constructor, `ConfigureServices` and `Configure` methods.

    * [Hello World with custom startup class name](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-startup-custom-name)

      You don't have to call your startup class `Startup`. Any valid C# class will do.

    * [Hello World with responding to multiple urls](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-startup-basic-multiple-urls)

      Configure so that your web app responds to multiple urls.

    * [Hello World with multiple startups](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-startup-basic-multiple)

      This project highlights the fact that you can create multiple Startup classes and choose them at start depending on your needs. 

    * [Hello World with multiple startups using environment](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-startup-basic-multiple-environment)

      This project highlights the fact that you can create multiple startup classes and choose them at start depending on your needs depending on your environment (You do have to name the startup class with Startup). 

    * [Hello World with multiple Configure methods based on environment](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-startup-multiple-configure-environment)

      This project demonstrates the ability to pick `Configure` method in a single Startup class based on environment.

    * [Hello World with multiple ConfigureServices methods based on environment](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-startup-multiple-configure-environment-services)

      This project demonstrates the ability to pick `ConfigureServices` method in a single Startup class based on environment.

    * [Hello World with an implementation of IStartup interface](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-startup-istartup)

      We are really getting into the weed of startup right now. This is an example on how to implement `IStartup` directly. 

    * [Hello World without a startup class](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-no-startup)

      Why? just because we can.

    * [Hello world with IStartupFilter](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-startup-istartupfilter)

      Use `IStartupFilter` to configure your middleware. This is an advanced topic. [This article](https://andrewlock.net/exploring-istartupfilter-in-asp-net-core/) tries at explaining `IStartupFilter`. I will add more samples so `IStartupFilter` can be clearer.
      

  * [Show errors during startup](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-startup-capture-errors)

    Show a detailed report on exceptions that happen during the startup phase of your web app. It is very useful during development.

  * [Show Connection info](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-connection-info)

    Enumerate the connection information of a HTTP request.

  * [Environmental settings](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-env-development)

    Set your application environment to `Development` or `Production` or other mode directly from code. 

  * [Console logging](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-logging)

    Setup a basic logging in your app and show it to console.

    We add the following dependencies ```"Microsoft.Extensions.Logging": "1.1.0"``` and ```"Microsoft.Extensions.Logging.Console": "1.1.0"```

    We are using ```IApplicationBuilder Run```, an extension method for adding terminal middleware.

  * [Console logging - without framework log messages](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-logging-filtered)

    Filter out frameworking logging from your log output. Without filtering, logging can get very annoying becuase the framework produces a lot of messages.

  * [Hello World with middlewares](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-middleware)

    ASPNetCore is built on top of pipelines of functions called middleware. 
    
    We are using ```IApplicationBuilder Use```, an extension method for adding middleware and ```IApplicationBuilder Run```.

  * [IApplicationLifetime](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-IApplicationLifetime)

    Respond to application startup and shutdown.

    We are using ```IApplicationLifetime``` that trigger events during application startup and shutdown.

  * [IHostingEnvironment](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-IHostingEnvironment)

    `IHostingEnvironment` is available at `Startup` constuctor and `Startup.Configure`. This sample shows all the properties available in this interface.  

  * [Application Environment](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-application-environment)

    Requires `Microsoft.Extensions.PlatformAbstractions" : "1.1.0-*` dependency. This sample shows how to obtain application environment information (target framework, etc).

  * [Adding HTTP Response Header](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-header)

    Demonstrate on how to add a response header and where is allowed place to do it.

* **Request(7)**
  
  This section shows all the different ways you capture input and examine request to your web application.

  * **HTTP Verb (1)**
    * [Get request verb](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-startup-basic-request-verb)
      
      Detect the verb/method of the current request. 

  * **Headers (2)**
    * [Access Request Headers](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-startup-basic-request-headers)
      
      Enumerate all the available headers in a request.

    * [Type Safe Access to Request Headers](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-startup-basic-request-headers-typed)
      
      Instead of using string to access HTTP headers, use type safe object properties to access common HTTP headers.

  * **Form (2)**
    
    We take dependency on ```"Microsoft.AspNetCore.Routing" : "1.1.0-*"``` to enable routing facilities to make the form handling easier.

    * [Form Values](https://github.com/dodyg/practical-aspnetcore/tree/master/form-values) 
      
      Handles the values submitted via a form.

    * [Form Upload File](https://github.com/dodyg/practical-aspnetcore/tree/master/form-upload-file) 
      
      Upload a single file and save it to the current directory (check out the usage of ```.UseContentRoot(Directory.GetCurrentDirectory())```)

  * **Cookies (2)**
          
      * [Cookies](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-cookies)

        Read and write cookies.

      * [Removing cookies](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-cookies-2)

        Simply demonstrates on how to remove cookies.

* **Routing (9)**

  We take dependency on ```"Microsoft.AspNetCore.Routing" : "1.1.0-*"``` to enable routing facilities in your aspnetcore apps.
  There are several samples to illuminate this powerful library.

  * [Router](https://github.com/dodyg/practical-aspnetcore/tree/master/routing)
  
    A single route handler that handles every path request.

  * [Router 2](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-2)
  
    Two route handler, one for home page (/) and the other takes the rest of the request using asterisk (*) in the url template.

  * [Router 3](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-3)

    We are exploring default handler - this is the entry point to create your own framework.
    
  * [Router 4](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-4)

    We are mixing optional route parameter, route parameter with default value and default handler.

  * [Router 5]
    
    This is still broken. I am trying to figure out how to do nested routing. Wish me luck!
  
  * [Router 6](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-6)

    We are building a template route segment by segment and parts by parts, oldskool. We are using ```TemplateMatcher```, ```TemplateSegment``` and ```TemplatePart```. 

    Hold your mask, we are going deep.
  
  * [Router 7](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-7)

    We are creating a routing template with two segments, one with Literal part and the other Parameter part, e.g, "/page/{*title}"

  * [Router 8](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-8)

    We are creating a routing template with one segment consisted of two parts, one Literal and one Parameter, e.g. "/page{*title}". Note the difference between this example and Router 7.

  * [Router 9](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-9)
   
    I am still trying to determine whether `TemplateMatcher` uses the `InlineConstraint` information.

    Update: No, `TemplateMatcher` does not run constraints. [#362](https://github.com/aspnet/Routing/issues/362)
 
  * [Router 10](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-10)    

    We have been building a `RouteTemplate` manually using `TemplateSegment` and `TemplatePart`. In this example we are using `TemplateParser` to build the `RouteTemplate` using string.

* **Middleware (8)**

  We will explore all aspect of middleware building in this section. There is no extra dependency taken other than `Kestrel` and `dotnet watch`. 

  * [Middleware 1](https://github.com/dodyg/practical-aspnetcore/tree/master/middleware-1)
   
    This example shows how to pass information from one middleware to another using `HttpContext.Items`.

  * [Middleware 3](https://github.com/dodyg/practical-aspnetcore/tree/master/middleware-3)
   
    This is the simplest middleware class you can create. 

  * [Middleware 4](https://github.com/dodyg/practical-aspnetcore/tree/master/middleware-4)
   
    Use `app.Map` (`MapMiddleware`) to configure your middleware pipeline to respond only on specific url path.

  * [Middleware 5](https://github.com/dodyg/practical-aspnetcore/tree/master/middleware-5)
   
    Nested `app.Map` (show `Request.Path` and `Request.PathBase`).

  * [Middleware 6](https://github.com/dodyg/practical-aspnetcore/tree/master/middleware-6)
   
    Use `app.MapWhen`(`MapWhenMiddleware`) and Nested `app.Map` (show `Request.Path` and `Request.PathBase`).

  * [Middleware 7](https://github.com/dodyg/practical-aspnetcore/tree/master/middleware-7)
   
    Use `MapMiddleware` and `MapWhenMiddleware` directly without using extensions (show `Request.Path` and `Request.PathBase`).

  * [Middleware 8](https://github.com/dodyg/practical-aspnetcore/tree/master/middleware-8)
   
    Demonstrate the various ways you can inject dependency to your middleware class *manually*. 

  * [Middleware 9](https://github.com/dodyg/practical-aspnetcore/tree/master/middleware-9)
   
    Demonstrate how to ASP.NET Core built in DI (Dependency Injection) mechanism to provide dependency for your middleware.


* **Features (7)**
  
  Features are collection of objects you can obtain from the framework at runtime that serve different purposes.

  * [Server Addresses Feature](https://github.com/dodyg/practical-aspnetcore/tree/master/features-server-addresses)

    Use this Feature to obtain a list of urls that your app is responding to.

  * [Request Feature](https://github.com/dodyg/practical-aspnetcore/tree/master/features-server-request)

    Obtain details of a current request. It has some similarity to HttpContext.Request. They are not equal. `HttpContext.Request` has more properties.  

  * [Connection Feature](https://github.com/dodyg/practical-aspnetcore/tree/master/features-connection)

    Use `IHttpConnectionFeature` interface to obtain local ip/port and remote ip/port. 

  * [Custom Feature](https://github.com/dodyg/practical-aspnetcore/tree/master/features-server-custom)

    Create your own custom Feature and pass it along from a middleware. 

  * [Custom Feature - Override](https://github.com/dodyg/practical-aspnetcore/tree/master/features-server-custom-override)

    Shows how you can replace an implementation of a Feature with another within the request pipeline.

  * [Request Culture Feature](https://github.com/dodyg/practical-aspnetcore/tree/master/features-request-culture)

    Use this feature to detect the culture of a web request through `IRequestCultureFeature`. It needs the following dependency `"Microsoft.AspNetCore.Localization": "1.1.0"`.

  * [Session Feature](https://github.com/dodyg/practical-aspnetcore/tree/master/features-session)

    Use session within your middlewares. This sample shows a basic usage of in memory session. It needs the following dependency '"Microsoft.AspNetCore.Session" : "1.1.0-*"` and `"Microsoft.Extensions.Caching.Memory" : "1.1.0-*"`.

* **Dependency Injection (2)**

  ASP.NET Corenetcore lives and die by DI. It relies on `Microsoft.Extensions.DependencyInjection` library. There is no need to put this dependency in your `project.json` explicitly because aspnetcore already has this package as its own dependency.

  * [Dependency Injection 1 - The basic](https://github.com/dodyg/practical-aspnetcore/tree/master/dependency-injection-1)

    Demonstrate the three lifetime registrations for the out of the box DI functionality: singleton (one and only forever), scoped (one in every request) and transient (new everytime).

  * [Dependency Injection 3 - Easy registration](https://github.com/dodyg/practical-aspnetcore/tree/master/dependency-injection-3)
  
    Register all objects configured by classes that implements a specific interface (`IBootstrap` in this example). This is useful when you have large amount of classes in your project that needs registration. You can register them near where they are (usually in the same folder) instead of registering them somewhere in a giant registration function.

    Note: example 2 is forthcoming. The inspiration has not arrived yet.

* **File Provider (2)**
  
  We will deal with various types of file providers supported by ASP.NET Core

  * [Physical File Provider - Content and Web roots](https://github.com/dodyg/practical-aspnetcore/tree/master/file-provider-physical)

    Access the file information on your Web and Content roots. 

  * [Custom File Provider](https://github.com/dodyg/practical-aspnetcore/tree/master/file-provider-custom)

    Implement a simple and largely nonsense file provider. It is a good starting point to implement your own proper File Provider.
    

* **In Memory Caching (a.k.a local cache) (4)**

  These samples depends on `Microsoft.Extensions.Caching.Memory` library. Please add this dependency to your `project.json`.

  * [Caching - Absolute/Sliding expiration](https://github.com/dodyg/practical-aspnetcore/tree/master/caching)

    This is the most basic caching you can use either by setting absolute or sliding expiration for your cache. Absolute expiration will remove your cache at a certain point in the future. Sliding expiration will remove your cache after period of inactivity.

  * [Caching 2 - File dependency](https://github.com/dodyg/practical-aspnetcore/tree/master/caching-2)
    
    Add file dependency to your caching so when the file changes, your cache expires.

    You need to put the cache file in your `project.json` so it gets copied over, e.g.

    `"buildOptions": {
        "emitEntryPoint": true,
        "copyToOutput": ["cache-file.txt"]
    }`

    Note: example 1 is forthcoming. The inspiration has not arrived yet.

  * [Caching 3 - Cache removal event](https://github.com/dodyg/practical-aspnetcore/tree/master/caching-3)

    Register callback when a cached value is removed.

  * [Caching 4 - CancellationChangeToken dependency](https://github.com/dodyg/practical-aspnetcore/tree/master/caching-4)

    Bind several cache entries to a single dependency that you can reset manually.

* **Configuration (7)**

  This section is all about configuration, from memory configuration to INI, JSON and XML.

  * [Configuration](https://github.com/dodyg/practical-aspnetcore/tree/master/configuration)

    This is the 'hello world' of configuration. Just use a memory based configuration and read/write values to/from it.

  * [Configuration - Options](https://github.com/dodyg/practical-aspnetcore/tree/master/configuration-options)

    Use IOptions at the most basic.

  * [Configuration - Environment variables](https://github.com/dodyg/practical-aspnetcore/tree/master/configuration-environment-variables)

    Load environment variables and display all of them.

  * [Configuration - INI file](https://github.com/dodyg/practical-aspnetcore/tree/master/configuration-ini)

    Read from INI file. It requires taking a new dependency, `"Microsoft.Extensions.Configuration.INI" : "1.1.0"`.

  * [Configuration - INI file - Options](https://github.com/dodyg/practical-aspnetcore/tree/master/configuration-ini-options)

    Read from INI file (with nested keys) and IOptions. It requires taking two new dependencies, `"Microsoft.Extensions.Configuration.INI" : "1.1.0"` and `"Microsoft.Extensions.Options.ConfigurationExtensions" : "1.1.0"`.

  * [Configuration - XML file](https://github.com/dodyg/practical-aspnetcore/tree/master/configuration-xml)

    Read from XML file. It requires taking a new dependency, `"Microsoft.Extensions.Configuration.Xml" : "1.1.0"`.

    **Note**: This Xml Configuration provider does not support repeated element.

    The following configuration settings will break:

    ```
    <appSettings>
      <add key="webpages:Version" value="3.0.0.0" />
      <add key="webpages:Enabled" value="false" />
    </appSettings>
    ```

    On the other hand you can get unlimited nested elements and also attributes.

  * [Configuration - XML file - Options](https://github.com/dodyg/practical-aspnetcore/tree/master/configuration-xml-options)

    Read from XML file and use IOptions. It requires taking two new dependencies, `"Microsoft.Extensions.Configuration.Xml" : "1.1.0"` and `"Microsoft.Extensions.Options.ConfigurationExtensions" : "1.1.0"`.

* **Localization and Globalization (4)**

  This section is all about languages, culture, etc.

  * [Localization](https://github.com/dodyg/practical-aspnetcore/tree/master/localization)

    Shows the most basic use of localization using a resource file. This sample only supports French language (because we are fancy). It needs the following dependency `"Microsoft.AspNetCore.Localization": "1.1.0"` and  `"Microsoft.Extensions.Localization": "1.1.0"`.

  * [Localization - 2](https://github.com/dodyg/practical-aspnetcore/tree/master/localization-2)

    We build upon the previous sample and demonstrate how to switch request culture via query string using the built in `QueryStringRequestCultureProvider`. This sample supports English and French.

  * [Localization - 3](https://github.com/dodyg/practical-aspnetcore/tree/master/localization-3)

    Demonstrate the difference between `Culture` and `UI Culture`.

  * [Localization - 4](https://github.com/dodyg/practical-aspnetcore/tree/master/localization-4)

    Demonstrate how to switch request culture via cookie using the built in `CookieRequestCultureProvider`. This sample supports English and French.

* **URL Redirect/Rewriting (6)**
  
  This section explore the dark arts of URL Rewriting

  * [Rewrite](https://github.com/dodyg/practical-aspnetcore/tree/master/rewrite)
    
    Shows the most basic of URL rewriting which will **redirect** (returns [HTTP 302](https://en.wikipedia.org/wiki/HTTP_302)) anything to the home page "/". It requires `"Microsoft.AspNetCore.Rewrite" : "1.0.0-*"` and `"Microsoft.AspNetCore.Routing" : "1.1.0-*"` dependencies. These two dependencies apply to the rest of the samples in this category.
    
    If you have used routing yet, I recommend of checking out the routing examples.

  * [Rewrite - 2](https://github.com/dodyg/practical-aspnetcore/tree/master/rewrite-2)
    
    **Redirect** (returns [HTTP 302](https://en.wikipedia.org/wiki/HTTP_302)) anything with an extension e.g. about-us.html or welcome.aspx to home page (/). It also shows how to capture the matched regex values.

  * [Rewrite - 3](https://github.com/dodyg/practical-aspnetcore/tree/master/rewrite-3)

    **Rewrite** anything with an extension e.g. about-us.html or welcome.aspx to home page (/). It also shows how to capture the matched regex values.
    
  * [Rewrite - 4](https://github.com/dodyg/practical-aspnetcore/tree/master/rewrite-4)
    
    **Permanent Redirect** (returns [HTTP 301](https://en.wikipedia.org/wiki/HTTP_301)) anything with an extension e.g. about-us.html or welcome.aspx to home page (/). It also shows how to capture the matched regex values.

  * [Rewrite - 5](https://github.com/dodyg/practical-aspnetcore/tree/master/rewrite-5)
  
    Implement a custom redirect logic based on `IRule` implementation. Require additional dependency of `"Microsoft.AspNetCore.StaticFiles": "1.1.0"` to serve images.

    This custom redirection logic allows us to simply specify the image file names without worrying about their exact path e.g.'xx.jpg' and 'yy.png'.

  * [Rewrite - 6](https://github.com/dodyg/practical-aspnetcore/tree/master/rewrite-6)
  
    Implement a custom redirect logic using lambda (similar functionality to Rewrite - 5). Require additional dependency of `"Microsoft.AspNetCore.StaticFiles": "1.1.0"` to serve images.

    This custom redirection logic allows us to simply specify the image file names without worrying about their exact path e.g.'xx.jpg' and 'yy.png'.

* **Compression (1)**

  Enable the ability to compress ASP.NET Core responses. These samples takes a dependency of ```Microsoft.AspNetCore.ResponseCompression": "1.0.1```.

  * [Default Gzip Output Compression](https://github.com/dodyg/practical-aspnetcore/tree/master/compression-response) 
   
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

* **Diagnostics(5)**

  These samples take a dependency of ```"Microsoft.AspNetCore.Diagnostics":"1.1.1"```. 

  * [Welcome Page](https://github.com/dodyg/practical-aspnetcore/tree/master/diagnostics)

    Simply show a welcome page to indicate that the app is working properly. This sample does not use a startup class simply because it's just a one line code.

  * [Developer Exception Page](https://github.com/dodyg/practical-aspnetcore/tree/master/diagnostics-2)

    Show any unhandled exception in a nicely formatted page with error details. Only use this in development environment!


  * [Custom Global Exception Page](https://github.com/dodyg/practical-aspnetcore/tree/master/diagnostics-3)

    Use ```IExceptionHandlerFeature``` feature provided by ```Microsoft.AspNetCore.Diagnostics.Abstractions``` to create custom global exception page.

  * [Custom Global Exception Page - 2 ](https://github.com/dodyg/practical-aspnetcore/tree/master/diagnostics-4)

    Similar to the previous one except that that we use the custom error page defined in separate path.

  * [Status Pages ](https://github.com/dodyg/practical-aspnetcore/tree/master/diagnostics-5)

    Use ```UseStatusCodePagesWithRedirects```.  **Beware:** This extension method handles your 5xx return status code by redirecting it to a specific url. It will not handle your application exception in general (for this use ```UseExceptionHandler``` - check previous samples).


* **Static Files(4)**

    This additional dependency is required to enable the functionality ```"Microsoft.AspNetCore.StaticFiles": "1.1.0"```. 

  * [Serve static files](https://github.com/dodyg/practical-aspnetcore/tree/master/serve-static-files)

    Simply serve static files (html, css, images, etc).     
    
    There are two static files being served in this project, index.html and hello.css. They are stored under ```wwwroot``` folder, which is the default folder location for this library. 
    
    To access them you have to refer them directly e.g. ```localhost:5000/index.html``` and ```localhost:5000/hello.css```.

  * [Allow Directory Browsing](https://github.com/dodyg/practical-aspnetcore/tree/master/serve-static-files-2)
    
    Allow listing and browsing of your ```wwwroot``` folder.

  * [Use File Server](https://github.com/dodyg/practical-aspnetcore/tree/master/serve-static-files-3)
    
    Combines the functionality of ```UseStaticFiles, UseDefaultFiles, and UseDirectoryBrowser```.

  * [Custom Directory Formatter](https://github.com/dodyg/practical-aspnetcore/tree/master/serve-static-files-4)
    
    Customize the way Directory Browsing is displayed. In this sample the custom view only handles flat directory. We will deal with 
    more complex scenario in the next sample.

* **Misc (3)**

  * [Markdown server](https://github.com/dodyg/practical-aspnetcore/tree/master/markdown-server)

    Serve markdown file as html file. You will see how you can create useful app using a few basic facilities in aspnetcore.

    We take ```"CommonMark.Net" : "0.13.4"``` as dependency. 
    
  * [Markdown server - implemented as middleware component](https://github.com/dodyg/practical-aspnetcore/tree/master/markdown-server-middleware)

    Serve markdown file as html file. It has the same exact functionality as [Markdown server](https://github.com/dodyg/practical-aspnetcore/tree/master/markdown-server) but implemented using middleware component.

    We take ```"CommonMark.Net" : "0.13.4"``` as dependency. 

    [Check out](https://docs.asp.net/en/latest/migration/http-modules.html) the documentation on how to write your own middleware.

  * [Password Hasher server](https://github.com/dodyg/practical-aspnetcore/tree/master/password-hasher)

    Give it a string and it will generate a secure hash for you, e.g. ```localhost:5000?password=mypassword```.

    We add dependency ```"Microsoft.AspNetCore.Identity": "1.1.0-*"``` to enable this functionality.


## Other resources

These are other aspnetcore resources with code samples

* [aspnetcore documentation](https://github.com/aspnet/Docs/tree/master/aspnet/fundamentals)
* [aspnetcore entropy](https://github.com/aspnet/entropy)


## Misc

* [Contributor Guidelines](https://github.com/dodyg/practical-aspnetcore/blob/master/CONTRIBUTING.md)
* [Code of Conduct](https://github.com/dodyg/practical-aspnetcore/blob/master/CODE_OF_CONDUCT.md)
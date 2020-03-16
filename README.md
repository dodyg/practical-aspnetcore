# Samples for ASP.NET Core 3.1 (WIP)

This branch in a work in progress. I am converting over 200 samples from previous versions to 3.1. If you are starting on ASP.NET Core, you can start from the samples from the master branch. 

## Additional Sections

*Continue to scroll down to find materials for absolute beginners to ASP.NET Core.*

| Sections                                                        |                                                           |
|-----------------------------------------------------------------|-----------------------------------------------------------|
| [Blazor Client Side (Web Assembly)](/projects/blazor/README.md) | [Middleware](/projects/middleware)    |
| [Blazor Server Side](/projects/blazor-ss)                       | [MVC](/projects/mvc)                                      |
| [Caching](/projects/caching)                                    | [Razor Pages](/projects/razor-pages)                      |
| [Configurations](/projects/configurations)                      | [Orchard Core](/projects/orchard-core)                    |
| [Dependency Injection](/projects/dependency-injection/)         | [Request](/projects/request)                              |
| [Diagnostics](/projects/diagnostics)                            | [Response](/projects/response)                            |
| [Endpoint Routing](/projects/endpoint-routing)                  | [Static Files and File Provider](/projects/file-provider) |
| [Features](/projects/features)                                  | [System.Text.Json](/projects/json)                        |
| [Health Check](/projects/health-check)                          | [Startup](/projects/startup)                              |
| [IHttpClientFactory](/projects/httpclientfactory)               | [URL Redirect/Rewrite](/projects/rewrite)                 |
| [IHostedService](/projects/ihosted-service)                     | [Syndications](/projects/syndications)                    |
| [Generic Hosting](/projects/generic-host)                       | [Uri Helper](/projects/uri-helper)                        |
| [gRPC](/projects/grpc)                                          | [Web Sockets](/projects/web-sockets)                      |
| [Logging](/projects/logging)                                    | [Web Utilities](/projects/web-utilities)                  |

## How to run these samples

To run these samples, simply open your command line console,  go to each folder and execute `dotnet watch run`.

## Foundation ASP.NET Core 3.1 Samples

### Basic

* [Hello World](/projects/basic/hello-world)

  This is the simplest ASP.NET Core application you can create. An ASP.NET Core application includes a super fast web server called Kestrel. In a few lines of code we set up the web server and a simple app.

  In this sample we use a `Startup` class to configure your application. This is the canonical way of doing thing.
  
* [Hello World - 2](/projects/basic/hello-world-2)

  This is the equivalent of the previous Hello World sample except that in this case we don't use a `Startup` class. This way of configuring things is not common.  

* [IWebHostConfiguration](/projects/basic/i-webhost-environment)

  This sample demonstrates the usage of `IWebHostEnvironment` from `Configure` method.

* [IHostEnvironment](/projects/basic/i-host-environment)

  This sample shows how to access `IHostEnvironment` from `ConfigureServices`. 

* [IConfiguration](/projects/basic/iconfiguration)

  This sample demonstrates the usage of `IConfiguration` from `Configure' method.

### Misc
* [Application Environment](/projects/application-environment)

  This sample shows how to obtain application environment information (target framework, etc).
  
* [Show Connection info](/projects/connection-info)

  Enumerate the connection information of a HTTP request.

* [Keeping track of anonymous users](/projects/anonymous-id)

  Keep track of anonymous user in your ASP.NET Core (useful in scenario such as keeping track of shopping cart) using `ReturnTrue.AspNetCore.Identity.Anonymous` library.

* [Password Hasher server](/projects/password-hasher)

  Give it a string and it will generate a secure hash for you, e.g. ```localhost:5000?password=mypassword```.

* [Integrate Newtonsoft.Json back](/projects/newtonsoft-json)

  ASP.NET Core has a new built in JSON Serializer/Deserializer. This sample shows how to integrate Newtonsoft.Json back to your project.

* [Version info](/projects/version)
 
  Show various version info of the framework your system is running on.

* [IApplicationLifetime](/projects/i-application-lifetime)

  Responds to application startup and shutdown.

  We are using ```IApplicationLifetime``` that trigger events during application startup and shutdown.

### Server-Sent Events

* [Forever Server](/projects/sse)

  This server will send a 'hello world' greeting forever.

### Markdown

* [Markdown server](/projects/markdown-server)

  Serve markdown file as html file. You will see how you can create useful app using a few basic facilities in aspnetcore.

  We take ```"Markdig"``` as dependency. 
    
* [Markdown server - implemented as middleware component](/projects/markdown-server-middleware)

  Serve markdown file as html file. It has the same exact functionality as [Markdown server](/projects/markdown-server) but implemented using middleware component.

  We take ```"Markdig"``` as dependency. 

### Utils

* [Status Codes](/projects/http-status-codes)

  Here we contrast between the usage of `Microsoft.AspNetCore.Http.StatusCodes` and `System.Net.HttpStatusCode`.

* [MediaTypeNames](/projects/media-type-names)

  This class provides convenient constants for some common MIME types. It's not extensive by any means however `MediaTypeNames.Text.Html` and `MediaTypeNames.Application.Json` come handy.  

* [MediaTypeNames - 2](/projects/media-type-names-2)

  Using `FileExtensionContentTypeProvider` to obtain the correct MIME type of a filename extension.

### Device Detection
  
The samples in this section rely on [Wangkanai.Detection](https://github.com/wangkanai/Detection) library.

* [Device Detection](/projects/device-detection)

  This is the most basic device detection. You will be able to detect whether the client is a desktop or a mobile client.


### Image Sharp

  All these samples require `SixLabors.ImageSharp.Web` middleware package. This middleware is an excelent tool to process your day to day image processing need.

  * [Image-Sharp](/projects/image-sharp)

    This example shows how to enable image resizing functionality to your site. It's super easy and the middleware takes care of caching, etc.


<!-- 

* **Hello World (22)**

* **Trimming (1)**
  
  This section shows the various way on how to trim the size of your application by using [Microsoft.Packagin.Tools.Trimming](https://www.nuget.org/packages/Microsoft.Packaging.Tools.Trimming/1.1.0-preview1-26619-01)

  * [Trimming Microsoft.AspNetCore.All hello world application](/projects/hello-world-startup-all-package-trimming)

    Run ```dotnet publish``` or ```dotnet build``` and read the output in your terminal. It will read something similar to ```Trimmed 115 out of 168 files for a savings of 18.93 MB Final app size is 3.07 MB```. You can turn off the trimming by setting ```<TrimUnusedDependencies>true</TrimUnusedDependencies>``` to ```false``` at the project file.

* **Email (1)**

  This section shows samples of using [MailKit](https://github.com/jstedfast/MailKit), which is essentially **the** library to use for sending and receiving email in ASP.NET Core.

  * [Send email](/projects/mailkit)
  
    This shows an example on how to send an email using SMTP.

    Thanks to [@Kinani95](https://twitter.com/Kinani95).

  
-->

## Misc

* [Contributor Guidelines](https://github.com/dodyg/practical-aspnetcore/blob/master/CONTRIBUTING.md)
* [Code of Conduct](https://github.com/dodyg/practical-aspnetcore/blob/master/CODE_OF_CONDUCT.md)
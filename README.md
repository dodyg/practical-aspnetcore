# Samples for ASP.NET Core 5.0 (391)

While I got your attention, Palestine needs helps. Support [Palestine Children's Relief Fund](https://www.charitynavigator.org/index.cfm?bay=search.summary&orgid=6421).

The goal of this project is to enable .NET programmers to learn the new ASP.NET Core stack from the ground up directly from code. There is so much power in the underlying ASP.NET Core stack. Don't miss them!

I highly recommend using [Visual Studio Code](https://code.visualstudio.com/) to play around with these samples but it is not required. You can use Visual Studio 2019 as well.

## Previous versions

[3.1 LTS](https://github.com/dodyg/practical-aspnetcore/tree/3.1-LTS/), [2.1 LTS](https://github.com/dodyg/practical-aspnetcore/tree/2.1-LTS)

## .NET 6 RC2 Section (51)

You can access .NET 6 RC2 specific samples [here](projects/net6).

## Sections


| Section                                                                                    |      |                                                                                                      |      |
| ------------------------------------------------------------------------------------------ | ---- | ---------------------------------------------------------------------------------------------------- | ---- |
| [Blazor Client Side (Web Assembly)](/projects/blazor/README.md) (Components, Data Binding) | (22) | [MVC](/projects/mvc)  (Localization, Routing, Razor Class Library, Tag Helpers, View Component, etc) | (47) |
| [Blazor Server Side](/projects/blazor-ss) (Localization)                                   | (15) | [Orchard Core](/projects/orchard-core)                                                               | (4)  |
| [Caching](/projects/caching)                                                               | (5)  | [Path String (HttpContext.Request.Path)](projects/path-string)                                       | (1)  |
| [Configurations](/projects/configurations)                                                 | (7)  | [Razor Pages](/projects/razor-pages)                                                                 | (9)  |
| [CoreWCF](/projects/corewcf)                                                               | (1)  | [Request](/projects/request) (Form, Cookies, Query String, Headers)                                  | (14) |
| [Dependency Injection](/projects/dependency-injection/)                                    | (4)  | [Response](/projects/response)                                                                       | (4)  |
| [Diagnostics](/projects/diagnostics)                                                       | (6)  | [SignalR](/projects/signalr)                                                                         | (1)  |
| [Endpoint Routing](/projects/endpoint-routing)                                             | (32) | [Security](/projects/security)                                                                       | (7)  |
| [Email](/projects/mailkit)                                                                 | (2)  | [Single File Application](projects/sfa)                                                              | (2)  |
| [Features](/projects/features)                                                             | (10) | [Static Files and File Provider](/projects/file-provider)                                            | (10) |
| [Generic Hosting](/projects/generic-host)                                                  | (9)  | [System.Text.Json](/projects/json)                                                                   | (11) |
| [gRPC](/projects/grpc) (including grpc-Web)                                                | (12) | [Startup](/projects/startup)                                                                         | (12) |
| [Health Check](/projects/health-check)                                                     | (6)  | [Syndications](/projects/syndications)                                                               | (3)  |
| [IHttpClientFactory](/projects/httpclientfactory)                                          | (4)  | [Testing](/projects/testing)                                                                        | (1)  |
| [IHostedService](/projects/ihosted-service)                                                | (2)  | [URL Redirect/Rewrite](/projects/rewrite)                                                            | (6)  |
| [Logging](/projects/logging)                                                               | (3)  | [Uri Helper](/projects/uri-helper)                                                                   | (5)  |
| [Localization and Globalization](projects/localization)                                    | (6)  | [Web Sockets](/projects/web-sockets)                                                                 | (5)  |
| [Middleware](/projects/middleware)                                                         | (14) | [Web Utilities](/projects/web-utilities)                                                             | (3)  |
| [Mini Apps](projects/mini)                                                                 | (1)  | [Orleans](projects/orleans)                                                                          | (12) |
|                                                                                            |      | [Xml](projects/xml)                                                                                  | (1)  |
|                                                                                            | 161  |                                                                                                      | 157  |

For Data Access samples, go to the excellent [ORM Cookbook](https://github.com/Grauenwolf/DotNet-ORM-Cookbook).
## How to run these samples

To run these samples, simply open your command line console,  go to each folder and execute `dotnet watch run`.

## Foundation ASP.NET Core 5 Samples

### Basic (6)

* [Hello World](/projects/basic/hello-world)

  This is the simplest ASP.NET Core application you can create. An ASP.NET Core application includes a super fast web server called Kestrel. In a few lines of code we set up the web server and a simple app.

  In this sample we use a `Startup` class to configure your application. This is the canonical way of doing thing.
  
* [Hello World - 2](/projects/basic/hello-world-2)

  This is the equivalent of the previous Hello World sample except that in this case we don't use a `Startup` class. This way of configuring things is not common.  

* [Hello World - 3](/projects/basic/hello-world-3)

  This sample uses C# 9 top level statements.

* [IWebHostConfiguration](/projects/basic/i-webhost-environment)

  This sample demonstrates the usage of `IWebHostEnvironment` from `Configure` method.

* [IHostEnvironment](/projects/basic/i-host-environment)

  This sample shows how to access `IHostEnvironment` from `ConfigureServices`. 

* [IConfiguration](/projects/basic/iconfiguration)

  This sample demonstrates the usage of `IConfiguration` from `Configure' method.

### Misc (8)
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

* [CommandLine](/projects/command-line/command-line-1)

  This project shows a simple integration between `System.CommandLine`, a command line parsing library with ASP.NET Core app.

### Server-Sent Events (1)

* [Forever Server](/projects/sse)

  This server will send a 'hello world' greeting forever.

### Markdown (2)

* [Markdown server](/projects/markdown-server)

  Serve markdown file as html file. You will see how you can create useful app using a few basic facilities in aspnetcore.

  We take ```"Markdig"``` as dependency. 
    
* [Markdown server - implemented as middleware component](/projects/markdown-server-middleware)

  Serve markdown file as html file. It has the same exact functionality as [Markdown server](/projects/markdown-server) but implemented using middleware component.

  We take ```"Markdig"``` as dependency. 

### Utils (3)

* [Status Codes](/projects/utils/http-status-codes)

  Here we contrast between the usage of `Microsoft.AspNetCore.Http.StatusCodes` and `System.Net.HttpStatusCode`.

* [MediaTypeNames](/projects/utils/media-type-names)

  This class provides convenient constants for some common MIME types. It's not extensive by any means however `MediaTypeNames.Text.Html` and `MediaTypeNames.Application.Json` come handy.  

* [MediaTypeNames - 2](/projects/utils/media-type-names-2)

  Using `FileExtensionContentTypeProvider` to obtain the correct MIME type of a filename extension.

### Device Detection (1)
  
The samples in this section rely on [Wangkanai.Detection](https://github.com/wangkanai/Detection) library.

* [Device Detection](/projects/device-detection)

  This is the most basic device detection. You will be able to detect whether the client is a desktop or a mobile client.


### Image Sharp (1)

  All these samples require `SixLabors.ImageSharp.Web` middleware package. This middleware is an excelent tool to process your day to day image processing need.

  * [Image-Sharp](/projects/image-sharp)

    This example shows how to enable image resizing functionality to your site. It's super easy and the middleware takes care of caching, etc.


## Misc

* [Contributor Guidelines](https://github.com/dodyg/practical-aspnetcore/blob/master/CONTRIBUTING.md)
* [Code of Conduct](https://github.com/dodyg/practical-aspnetcore/blob/master/CODE_OF_CONDUCT.md)

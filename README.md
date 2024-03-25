# Samples for ASP.NET Core 8.0 

> [!NOTE]
> This repository is WIP. I am updating all the previous samples from .NET 6/7 to .NET 8. Check the previous stable branch ([6.0](https://github.com/dodyg/practical-aspnetcore/tree/net6.0/)) if you want to browse the existing stable samples that contains also .NET 7/8 samples.

Greetings from Cairo, Egypt. You can [sponsor](https://github.com/sponsors/dodyg) this project [here](https://github.com/sponsors/dodyg). 

## Previous versions

[6.0](https://github.com/dodyg/practical-aspnetcore/tree/net6.0/), [5.0](https://github.com/dodyg/practical-aspnetcore/tree/net5.0/), [3.1 LTS](https://github.com/dodyg/practical-aspnetcore/tree/3.1-LTS/), [2.1 LTS](https://github.com/dodyg/practical-aspnetcore/tree/2.1-LTS)

## Sections

| Section                                                                 |     |                                                                              |
| ----------------------------------------------------------------------- | --- | ---------------------------------------------------------------------------- |
| [Authentication](/projects/authentication)                              | 5   |                                                                              |
| [Blazor Client Side (Web Assembly)](/projects/blazor-wasm)              | 22  | .NET8 (WIP), Components, Data Binding                                        |
| [Blazor Server](/projects/blazor-ss)                                    | 16  | Localization                                                                 |
| [Blazor Server Side Render](/projects/blazor-ssr)                       | 22  |                                                                              |
| [Caching](/projects/caching)                                            | 5   |                                                                              |
| [Configurations](/projects/configurations)                              | 10  |                                                                              |
| [CoreWCF](/projects/corewcf)                                            | 1   |                                                                              |
| [Dependency Injection](/projects/dependency-injection/)                 | 4   |                                                                              |
| [Diagnostics](/projects/diagnostics)                                    | 5   |                                                                              |
| [Endpoint Routing](/projects/endpoint-routing)                          | 32  |                                                                              |
| [Email](/projects/mailkit)                                              | 2   |                                                                              |
| [Elsa Workflow](/projects/elsa)                                         | 14  | .NET8                                                                        |
| [Exception Handler Middleware](/projects/exception-handler-middleware/) | 2   |                                                                              |
| [Features](/projects/features)                                          | 11  |                                                                              |
| [Generic Hosting](/projects/generic-host)                               | 9   |                                                                              |
| [gRPC](/projects/grpc) (including grpc-Web)                             | 12  |                                                                              |
| [Health Check](/projects/health-check)                                  | 6   |                                                                              |
| [IHttpClientFactory](/projects/httpclientfactory)                       | 4   |                                                                              |
| [IHostedService](/projects/ihosted-service)                             | 2   |                                                                              |
| [Logging](/projects/logging)                                            | 4   |                                                                              |
| [Localization and Globalization](/projects/localization)                | 6   |                                                                              |
| [Middleware](/projects/middleware)                                      | 14  |                                                                              |
| [Mini Apps](/projects/mini)                                             | 2   |                                                                              |
| [Minimal API](/projects/minimal-api)                                    | 36  | Routing, Parameter Bindings, etc                                             |
| [Minimal Hosting](/projects/minimal-hosting)                            | 23  |                                                                              |
| [MVC](/projects/mvc)                                                    | 47  | Localization, Routing, Razor Class Library, Tag Helpers, View Component, etc |
| [Output Cache Middleware](/projects/output-cache-middleware)             |     |                                                                              |
| [Open Telemetry](/projects/open-telemetry/)                             | 3   |                                                                              |
| [Orchard Core](/projects/orchard-core)                                  | 4   |                                                                              |
| [Path String (HttpContext.Request.Path)](/projects/path-string)         | 1   |                                                                              |
| [Problem Details Middleware](/projects/problem-details-middleware/)     | 3   |                                                                              |
| [Razor Pages](/projects/razor-pages)                                    | 10  | TempData                                                                     |
| [Request](/projects/request)                                            | 15  | Form, Cookies, Query String, Headers                                         |
| [Request Timeouts Middleware](/projets/request-timeouts-middleware)     | 6   |                                                                              |
| [Response](/projects/response)                                          | 3   |                                                                              |
| [RSS](/projects/rss-reader)                                             | 1   |                                                                              |
| [SignalR](/projects/signalr)                                            | 1   |                                                                              |
| [Security](/projects/security)                                          | 7   |                                                                              |
| [Single File Application](/projects/sfa)                                | 2   |                                                                              |
| [Static Files and File Provider](/projects/file-provider)               | 10  |                                                                              |
| [System.Text.Json](/projects/json)                                      | 22  |                                                                              |
| [Syndications](/projects/syndications)                                  | 3   |                                                                              |
| [Testing](/projects/testing)                                            | 1   |                                                                              |
| [URL Redirect/Rewrite](/projects/rewrite)                               | 6   |                                                                              |
| [Uri Helper](/projects/uri-helper)                                      | 5   |                                                                              |
| [Windows Service](/projects/windows-service)                            | 1   |                                                                              |
| [Web Sockets](/projects/web-sockets)                                    | 6   |                                                                              |
| [Web Utilities](/projects/web-utilities)                                | 3   |                                                                              |
| [Orleans](/projects/orleans)                                            | 5   | .NET.8                                                                       |
| [Xml](/projects/xml)                                                    | 1   |                                                                              |
| [YARP](/projects/yarp)                                                  | 1   |                                                                              |

For Data Access samples, go to the excellent [ORM Cookbook](https://github.com/Grauenwolf/DotNet-ORM-Cookbook). .NET team also has [a sample repository](https://github.com/dotnet/samples).

## How to run these samples

To run these samples, simply open your command line console, go to each folder and execute `dotnet watch run`.

### Misc (6)

-   [Application Environment](/projects/application-environment)

    This sample shows how to obtain application environment information (target framework, etc).

-   [Show Connection info](/projects/connection-info)

    Enumerate the connection information of a HTTP request.

-   [Password Hasher server](/projects/password-hasher)

    Give it a string and it will generate a secure hash for you, e.g. `localhost:5000?password=mypassword`.

-   [Version info](/projects/version)

    Show various version info of the framework your system is running on.

-   [IApplicationLifetime](/projects/i-application-lifetime)

    Responds to application startup and shutdown.

    We are using `IApplicationLifetime` that trigger events during application startup and shutdown.

- [Short Circuit](map-short-circuit)

  Use `MapShortCircuit` or `.ShortCircuit()` to efficiently respond to a request without going through a middleware pipeline run. 

### Server-Sent Events (1)

-   [Forever Server](/projects/sse)

    This server will send a 'hello world' greeting forever.

### Markdown (2)

-   [Markdown server](/projects/markdown-server)

    Serve markdown file as html file. You will see how you can create useful app using a few basic facilities in aspnetcore.

    We take `"Markdig"` as dependency.

-   [Markdown server - implemented as middleware component](/projects/markdown-server-middleware)

    Serve markdown file as html file. It has the same exact functionality as [Markdown server](/projects/markdown-server) but implemented using middleware component.

    We take `"Markdig"` as dependency.

### Utils (3)

-   [Status Codes](/projects/utils/http-status-codes)

    Here we contrast between the usage of `Microsoft.AspNetCore.Http.StatusCodes` and `System.Net.HttpStatusCode`.

-   [MediaTypeNames](/projects/utils/media-type-names)

    This class provides convenient constants for some common MIME types. It's not extensive by any means however `MediaTypeNames.Text.Html` and `MediaTypeNames.Application.Json` come handy.

-   [MediaTypeNames - 2](/projects/utils/media-type-names-2)

    Using `FileExtensionContentTypeProvider` to obtain the correct MIME type of a filename extension.

### Device Detection (1)

The samples in this section rely on [Wangkanai.Detection](https://github.com/wangkanai/wangkanai/tree/main/Detection) library.

-   [Device Detection](/projects/device-detection)

    This is the most basic device detection. You will be able to detect whether the client is a desktop or a mobile client.

### Image Sharp (1)

All these samples require `SixLabors.ImageSharp.Web` middleware package. This middleware is an excelent tool to process your day to day image processing need.

-   [Image-Sharp](/projects/image-sharp)

    This example shows how to enable image resizing functionality to your site. It's super easy and the middleware takes care of caching, etc.

## Misc

-   [Contributor Guidelines](https://github.com/dodyg/practical-aspnetcore/blob/master/CONTRIBUTING.md)
-   [Code of Conduct](https://github.com/dodyg/practical-aspnetcore/blob/master/CODE_OF_CONDUCT.md)

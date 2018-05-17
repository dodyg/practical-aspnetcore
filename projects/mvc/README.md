# MVC (5)

This section contains all micro samples for **ASP.NET Core MVC 2.1**.

Pre-requisite: Make sure you download .NET Core SDK 2.1 Preview 1 (`2.1.0-preview2-final`) otherwise below examples won't work.

There is no more need to include this tool in your project `<DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="2.1.0-preview1" />` to be able to enjoy `dotnet watch run`. It's included by default in ASP.NET Core 2.1.

* [Hello World Minimalistic](/projects/mvc/hello-world)
  This is the most basic hello world you can do using ASP.NET MVC. It uses `services.AddMvcCore()` with [behavior compatibility](https://blogs.msdn.microsoft.com/webdev/2018/02/27/introducing-compatibility-version-in-mvc/) set to `2.1` and with manually configured route.

  You shouldn't use this configuration for your typical ASP.NET MVC Core application (it does not support Razor, etc). This is just an example on how to do it with the least amount of functionality and configuration. You can find a discussion about `services.AddMvcCore()` [here](https://offering.solutions/blog/articles/2017/02/07/difference-between-addmvc-addmvcore/).


## Authentication/Authorization

* [JWT](/projects/mvc/jwt)

  Show how to generate and decode [JSON Web Tokens](https://jwt.io/).

## API

* [Use Microsoft.AspNetCore.Mvc.ProblemDetails](/projects/mvc/api-problem-details)

  Use `Microsoft.AspNetCore.Mvc.ProblemDetails` as part of your Web API error reply. It is implementing [RFC  7807](https://tools.ietf.org/html/rfc7807). It will make life easier for everybody.

## Formatters

* [Using Utf8Json as JSON Formatter](/projects/mvc/utf8json-formatter)

  Use the super fast [Ut8Json](https://github.com/neuecc/Utf8Json) JSON serialization library instead of the default one. This project requires `utf8json` and `Utf8Json.AspNetCoreMvcFormatter` packages. 

## Swagger (API Documentation)

* [Using NSwag](/projects/mvc/nswag)
  
  Generate automatic documentation for your Web API using [Swagger](https://swagger.io/) specification and [NSwag](https://github.com/RSuter/NSwag)

  You can further learn on how to customize the output of your documentation [here](https://github.com/domaindrivendev/Swashbuckle.AspNetCore#include-descriptions-from-xml-comments) and [here](http://michaco.net/blog/TipsForUsingSwaggerAndAutorestInAspNetCoreMvcServices).

  Do not forget to enable XML documentation XML generation for your project

  ``` xml
  <PropertyGroup>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
  </PropertyGroup>
  ```

  We also use the new `ActionResult<T>` return type. You can read more about the type [here](https://joonasw.net/view/aspnet-core-2-1-actionresult-of-t).
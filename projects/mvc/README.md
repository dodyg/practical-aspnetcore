# MVC (2)

This section contains all micro samples for **ASP.NET Core MVC 2.1**.

Pre-requisite: Make sure you download .NET Core SDK 2.1 Preview 1 (`2.1.0-preview1-final`) otherwise below examples won't work.

* [Hello World Minimalistic](/projects/mvc/hello-world)
  This is the most basic hello world you can do using ASP.NET MVC. It uses `services.AddMvcCore()` with [behavior compatibility](https://blogs.msdn.microsoft.com/webdev/2018/02/27/introducing-compatibility-version-in-mvc/) set to `2.1` and with manually configured route.

  You shouldn't use this configuration for your typical ASP.NET MVC Core application (it does not support Razor, etc). This is just an example on how to do it with the least amount of functionality and configuration. You can find a discussion about `services.AddMvcCore()` [here](https://offering.solutions/blog/articles/2017/02/07/difference-between-addmvc-addmvcore/).


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
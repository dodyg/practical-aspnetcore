# MVC (47)

This section contains all micro samples for **ASP.NET Core MVC 2.1**.

Pre-requisite: Make sure you download .NET Core SDK 2.1 (`2.1.0`) otherwise below examples won't work.

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

* [Extends Microsoft.AspNetCore.Mvc.ProblemDetails](/projects/mvc/api-problem-details-2)

  Extend `Microsoft.AspNetCore.Mvc.ProblemDetails` to make it easier for day to day use. It will adjust what kind of information it shows based on your development environment.

## Model Binding
  
  We are exploring everything related to model binding in this section.

  * [Model binding using a class and FromQuery attribute](/projects/mvc/model-binding-from-query)

    Use `[FromQuery]` attribute to have MVC put all the query string values nicely in a class instead of using primitives e.g. `int userId`.

  * [Model binding using a class and FromRoute attribute](/projects/mvc/model-binding-from-route)

    Use `[FromRoute]` attribute to have MVC put all the route values nicely in a class instead of using primitives e.g. `int userId`.


## Action Results
  
  We are exploring various  that an Action returns.

  * [FileStreamResult](/projects/mvc/result-filestream)

    An example on how to return a file to the browser when you have a stream available.  

  * [PhysicalFileResult](/projects/mvc/result-physicalfile)

    An example on how to return a file to the browser when you have a path to a file on disk.
 

## Formatters

* [Using Utf8Json as JSON Formatter](/projects/mvc/utf8json-formatter)

  Use the super fast [Ut8Json](https://github.com/neuecc/Utf8Json) JSON serialization library instead of the default one. This project requires `utf8json` and `Utf8Json.AspNetCoreMvcFormatter` packages.

* [Returning XML Response](/projects/mvc/mvc-output-xml)

  Return XML response using `Microsoft.AspNetCore.Mvc.Formatters.Xml`. 

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

* [Customizing NSwag](/projects/mvc/nswag-2)

  Use attribute such as `SwaggerTag` to organize your API or `SwaggerIgnore` to hide an API from the definition (using `[ApiExplorerSettings(IgnoreApi = true)]` also works).

## Tag Helpers

  * [Tag Helper - Hello World](/projects/mvc/tag-helper)

    This is the simplest tag helper you can do. It just prints 'hello world'.

  * [Tag Helper - Alert Tag Helper](/projects/mvc/tag-helper-2)

    Convert a message to become an alert message (bootstrap 4).

  * [Tag Helper - Alert Tag Helper With Style](/projects/mvc/tag-helper-3)

    Convert a message to become an alert message with 4 style of alerts (bootstrap 4).

  * [Tag Helper - Alert Tag Helper with other enclosed Tag Helpers](/projects/mvc/tag-helper-4)

    Demonstrate the usage of other tag helpers within the enclosure of an Alert Tag Helper.
    
  * [Tag Helper - Nested Alert Tag Helper](/projects/mvc/tag-helper-5)

    Demonstrate passing values from Parent Tag to Child Tag.


## Tag Helpers Tips and Tricks

  * [Cache Busting Tag Helper](/projects/mvc/tag-helper-link)

    Use `asp-append-version` to your css and script link to make sure that your visitors always use the latest version of your style and script files.

  * [Cache Busting Image Tag Helper](/projects/mvc/tag-helper-img)

    Use `asp-append-version` to your images to make sure that your visitors always use the latest version of images.


## Razor Class Library (3)

  We are exploring Razor Class Library (RCL) functionalities in this section. RCL allows you to create reusable UI libraries.

  * [Razor Class Library - Hello World](/projects/mvc/razor-class-library)

    This is the simplest example to demonstrate the functionality of RCL. The library uses Razor Pages. Go to `src/WebApplication` folder and run `dotnet watch run` to run the sample.

    Thanks to [@AdrienTorris](https://twitter.com/AdrienTorris).
 
  * [Razor Class Library - Include static files](/projects/mvc/razor-class-library-with-static-files)

    This is similar to previous example except now you can including static files (javascript, images, css, etc) with your RCL. Go to `src/WebApplication` folder and run `dotnet watch run` to run the sample.
    
    Thanks to [@AdrienTorris](https://twitter.com/AdrienTorris).

  * [Razor Class Library - using Controllers and Views](/projects/mvc/razor-class-library-with-controllers)

    This sample demonstrates on how to use Controllers and Views in your Razor Class Library in contrast to previous examples that uses Razor Pages.

    
    Thanks to [@AdrienTorris](https://twitter.com/AdrienTorris).

## Syndication Output Formatter (1)

  We are building a RSS/ATOM Output formatter starting from the very basic.

  * [Output Formatter Syndication](/projects/mvc/output-formatter-syndication)

    This is a very rudimentary RSS output formatter. It's valid but it does not do much other than providing RSS items.
  
# MVC (20)

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

## MVC and Pages Routing

  We are exploring every single boring details about MVC and Pages routing in this section.

  * [MVC Routing - 1](/projects/mvc/routing-1)

    Demonstrates fixed routing and default conventional routing for ASP.NET MVC.
  
  * [MVC Routing - 2](/projects/mvc/routing-2)

    Similar to `Routing - 1` but using alternative method to specify default conventional routing.

  * [MVC Routing - 3](/projects/mvc/routing-3)

    Use `UseMvcWithDefaultRoute` so you don't have to define the default conventional route. This is how the extension is [implemented](https://github.com/aspnet/Mvc/blob/e2de54a92d8254a27f9eefd77e08370c7b17fa5d/src/Microsoft.AspNetCore.Mvc.Core/Builder/MvcApplicationBuilderExtensions.cs).

    ``` csharp
    public static IApplicationBuilder UseMvcWithDefaultRoute(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        return app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
        });
    }
    ```

  * [MVC Routing - 4](/projects/mvc/routing-4)

    The simplest example for attribute routing. We use the `Route` attribute at the Controller. This only allows you to have one Action per Controller.

  * [MVC Routing - 5](/projects/mvc/routing-5)

    We use the `Route` attribute at Action methods (in contrast to previous example). This allows you to have multiple Actions in a Controller.

  * [MVC Routing - 6](/projects/mvc/routing-6)

    Demonstrate the usage of `HttpGet` and `HttpPost`.

  * [MVC Routing - 7](/projects/mvc/routing-7)

    Demonstrate the usage of `[controller]` replacement token at the `Route` attribute.
  
  * [MVC Routing - 8](/projects/mvc/routing-8)

    Demonstrate the usage of `[controller]` and `[action]` replacement tokens at the `Route` attribute.

  * [MVC Routing - 9](/projects/mvc/routing-9)

    Demonstrate the usage of `[action]` replacement tokens at the `HttpGet` attribute.

  * [MVC Routing - 10](/projects/mvc/routing-10)

    Demonstrate the usage of `IActionConstraint` attribute.

    The following map routing will search all HomeController.About action accross the assembly regardless of namespace. If you have multiple
    HomeController.About, it will generate error because the framework cannot decide which method to use. This sample demonstrates on how using
    a custom `IActionConstraint` attribute solves this problem.

    ``` csharp
    app.UseMvc(routes =>
    {
        routes.MapRoute(
        "About",
        "{id}/About",
        defaults: new { controller = "Home", Action = "About" });
    });
    ```

## Syndication Output Formatter

  We are building a RSS/ATOM Output formatter starting from the very basic.

  * [Output Formatter Syndication](/projects/mvc/output-formatter-syndication)

    This is a very rudimentary RSS output formatter. It's valid but it does not do much other than providing RSS items.
  
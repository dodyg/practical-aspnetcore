# MVC (47)

| Sections                                                       |     |
|----------------------------------------------------------------|-----|
| [MVC - Localization](/projects/mvc/localization)               | (10) |
| [MVC - Routing](/projects/mvc/routing)                         | (9) |
| [MVC - Razor Class Library](/projects/mvc/razor-class-library) | (3) |
| [MVC - Tag Helpers](/projects/mvc/tag-helper)                  | (7) |
| [MVC - View Component](/projects/mvc/view-component)           | (4) |
|                                                                | 33  |

* [Hello World Minimalistic](/projects/mvc/hello-world) (1)
  This is the most basic hello world you can do using ASP.NET MVC. It uses `services.AddMvcCore()` with [behavior compatibility](https://blogs.msdn.microsoft.com/webdev/2018/02/27/introducing-compatibility-version-in-mvc/) set to `2.1` and with manually configured route.

  You shouldn't use this configuration for your typical ASP.NET MVC Core application (it does not support Razor, etc). This is just an example on how to do it with the least amount of functionality and configuration. You can find a discussion about `services.AddMvcCore()` [here](https://offering.solutions/blog/articles/2017/02/07/difference-between-addmvc-addmvcore/).


## Authentication/Authorization (1)

* [JWT](/projects/mvc/jwt)

  Show how to generate and decode [JSON Web Tokens](https://jwt.io/).

## API (2)

* [Use Microsoft.AspNetCore.Mvc.ProblemDetails](/projects/mvc/api-problem-details)

  Use `Microsoft.AspNetCore.Mvc.ProblemDetails` as part of your Web API error reply. It is implementing [RFC  7807](https://tools.ietf.org/html/rfc7807). It will make life easier for everybody.

* [Extends Microsoft.AspNetCore.Mvc.ProblemDetails](/projects/mvc/api-problem-details-2)

  Extend `Microsoft.AspNetCore.Mvc.ProblemDetails` to make it easier for day to day use. It will adjust what kind of information it shows based on your development environment.

## Model Binding (2)
  
  We are exploring everything related to model binding in this section.

  * [Model binding using a class and FromQuery attribute](/projects/mvc/model-binding-from-query)

    Use `[FromQuery]` attribute to have MVC put all the query string values nicely in a class instead of using primitives e.g. `int userId`.

  * [Model binding using a class and FromRoute attribute](/projects/mvc/model-binding-from-route)

    Use `[FromRoute]` attribute to have MVC put all the route values nicely in a class instead of using primitives e.g. `int userId`.


## Action Results (3)
  
  We are exploring various  that an Action returns.

  * [FileStreamResult](/projects/mvc/result-filestream)

    An example on how to return a file to the browser when you have a stream available.  

  * [PhysicalFileResult](/projects/mvc/result-physicalfile)

    An example on how to return a file to the browser when you have a path to a file on disk.

  * [JsonResult](/projects/mvc/result-json)

    Makes it easy to returns JSON content from an action.
 

## Formatters (2)

* [Using Utf8Json as JSON Formatter](/projects/mvc/utf8json-formatter)

  Use the super fast [Ut8Json](https://github.com/neuecc/Utf8Json) JSON serialization library instead of the default one. This project requires `utf8json` and `Utf8Json.AspNetCoreMvcFormatter` packages.

* [Returning XML Response](/projects/mvc/mvc-output-xml)

  Return XML response using `Microsoft.AspNetCore.Mvc.Formatters.Xml`. 

## Swagger (API Documentation) (2)

* [Using NSwag](/projects/mvc/nswag)
  
  Generate automatic documentation for your Web API using [Swagger](https://swagger.io/) specification and [NSwag](https://github.com/RSuter/NSwag)

* [Customizing NSwag](/projects/mvc/nswag-2)

  Use attribute such as `OpenApiTag` to organize your API or `OpenApiIgnore` to hide an API from the definition (using `[ApiExplorerSettings(IgnoreApi = true)]` also works).

## Syndication Output Formatter (1)

  We are building a RSS/ATOM Output formatter starting from the very basic.

  * [Output Formatter Syndication](/projects/mvc/output-formatter-syndication)

    This is a very rudimentary RSS output formatter. It's valid but it does not do much other than providing RSS items.
  

# MVC (47)

| Sections                                                       |     |
| -------------------------------------------------------------- | --- |
| [MVC - Localization](/projects/mvc/localization)               | 10  |
| [MVC - Routing](/projects/mvc/routing)                         | 9   |
| [MVC - Razor Class Library](/projects/mvc/razor-class-library) | 3   |
| [MVC - Tag Helpers](/projects/mvc/tag-helper)                  | 7   |
| [MVC - View Component](/projects/mvc/view-component)           | 4   |
|                                                                | 33  |

* [Hello World](/projects/mvc/hello-world)
  A "hello world" MVC app.

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
 

## Formatters (1)

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
  
## Newtonsoft.Json (1)

  * [Using Newtonsoft.Json](/projects/mvc/newtonsoft-json)

    Use `Microsoft.AspNetCore.Mvc.NewtonsoftJson` to have MVC use `Newtonsoft.Json` instead of `System.Text.Json`.

dotnet6
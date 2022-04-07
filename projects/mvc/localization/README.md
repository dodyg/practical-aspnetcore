## Localization (10)

  We are exploring all the nitty gritty of localization with MVC here.

  * [MVC Localization - 1](/projects/mvc/localization/mvc-localization-1)

    Demonstrate the sample of naming resource file using the "." dot naming convention. In this case, our Assembly Name differs from our namespace so we use full type name in our resource. This allows `IStringLocalizer<>` to be used in Controllers.

  * [MVC Localization - 2](/projects/mvc/localization/mvc-localization-2)

    This sample is identical as previous example except that we are using the path naming convention. 
    
  * [MVC Localization - 3](/projects/mvc/localization/mvc-localization-3)

    Demonstrate an easy way to use shared resources. The class name, `Global`, is just a name. It can be `Common` or `CommonResources`, etc. It does not matter.

  * [MVC Localization - 4](/projects/mvc/localization/mvc-localization-4)

    Similar to `MVC Localization - 3` except that now the assembly name and namespace share the same name. This is in contrast to `MVC Localization - 1` and `MVC Localization`.

  * [MVC Localization - 5](/projects/mvc/localization/mvc-localization-5)

    Use shared resource on View.

  * [MVC Localization - 6](/projects/mvc/localization/mvc-localization-6)

    If you keep wondering why your default request language doesn't work, this example is for you.
    
    This example demonstrates on how to ignore browser language preference by removing `AcceptLanguageHeaderRequestCultureProvider` and forcing your default language. This [article](https://dotnetcoretutorials.com/2017/06/22/request-culture-asp-net-core/) has a useful explanation on this provider.

  * [MVC Localization - 7](/projects/mvc/localization/mvc-localization-7)

    This sample shows how to use localization resources located in a separate project. Notice how the namespace correspondents to the folder name at the resource project.

  * [MVC Localization - 8](/projects/mvc/localization/mvc-localization-8)

    This sample demonstrates the usage of `AcceptLanguageHeaderRequestCultureProvider` and `Accept-Language` HTTP header.

  * [MVC Localization - 9](/projects/mvc/localization/mvc-localization-9)

    This sample demonstrates the situation of `cultural fallback` behaviour - `Starting from the requested culture, if not found, it reverts to the parent culture of that culture.`[doc](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-3.1#culture-fallback-behavior).

  * [MVC Localization - 10](/projects/mvc/localization/mvc-localization-10)

    This sample uses `CustomRequestCultureProvider` to provide fine localization based on the first part of a url segment e.g. /en/my-page, /fr.

    dotnet6
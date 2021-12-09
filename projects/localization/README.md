# Localization and Globalization (6)

  This section is all about languages, culture, etc.

  * [Localization](/projects/localization/localization-1)

    Shows the most basic use of localization using a resource file. This sample only supports French language (because we are fancy). It needs the following dependency `"Microsoft.AspNetCore.Localization": "2.1.0"` and  `"Microsoft.Extensions.Localization": "2.1.0"`.

  * [Localization - 2](/projects/localization/localization-2)

    We build upon the previous sample and demonstrate how to switch request culture via query string using the built in `QueryStringRequestCultureProvider`. This sample supports English and French.

  * [Localization - 3](/projects/localization/localization-3)

    Demonstrate the difference between `Culture` and `UI Culture`.

  * [Localization - 4](/projects/localization/localization-4)

    Demonstrate how to switch request culture via cookie using the built in `CookieRequestCultureProvider`. This sample supports English and French.

  * [Localization - 5](/projects/localization/localization-5)

    Demonstrate using Portable Object (PO) files to support localization instead of the cumbersome resx file. This sample requires ```OrchardCore.Localization.Core``` package. This sample requires ```ASPNET Core 2```.

  * [Localization - 6](/projects/localization/localization-6)

    This is a continuation of previous sample but with context, which allows the same translation key to return different strings.


dotnet6
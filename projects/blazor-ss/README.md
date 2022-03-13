# Server Size Blazor (16)

This is an amazing piece of technology where your interactive web UI is handled via C# and streamed back and forth using web socket via SignalR.

All the samples in this section runs on SSL. If you have not gotten your local self-sign SSL in order yet, please read this [instruction](https://www.hanselman.com/blog/DevelopingLocallyWithASPNETCoreUnderHTTPSSSLAndSelfSignedCerts.aspx).

  * [Hello World](HelloWorld)

    This is the simplest Razor component app you can create. It will show you clearly the building block of a Razor component application.

    There are two extra settings for dotnet watch to monitor `*.cshtml` and `*.razor` file changes on two projects to make your development experience better.

    ``` xml
        <Watch Include="**\*.cshtml" />
        <Watch Include="**\*.razor" />
    ```

  * [Rss Reader](RssReader)

    This sample demonstrates that you can use normal server side packages with your Razor Component as it is a truly server side system. This sample uses `Microsoft.SyndicationFeed.ReaderWriter` package to parse an external RSS feed and display it.

  * [Rss Reader - 2](RssReader-2)
    
    This version of RSS Reader uses C# 8.0 `IAsyncEnumerable` to process RSS data as they are available. There is an artificial `await Task.Delay(3000);` added to `RssNews.GetNewsAsync` so you can see visually how the UI changes.

  * [Js Integration](JsIntegration)

    This sample shows how to access JavaScript functions available at `windows` global scope.

  * [Dependency Injection](DependencyInjection)

    This sample shows you that the 'Transient' and 'Scoped' Dependency Injection method have different practical impact on Razor Component.

  * [Layout](Layout)

    This sample shows how to use layout and nested layouts.

  * [Multiple Starting Points](StartingVariation)

    In this example we demonstrates how three different Razor Pages endpoints act as starting points for different path of your Server Side Blazor.

  * [Wall of Counters](WallOfCounters)

    In this sample we will use System.Timers.Timer to perform multiple counters. 
  
  * [ChatR](ChatR)

    In this sample we will host a SignalR Hub in the same host as the Blazor Server Side

  * [Component Events](ComponentEvents)

    In this sample we will facilitate communication between components and pages using a Scoped lifetime AppState object. 
 
  * [Component Events 2](ComponentEvents-2)

    This sample is similar to [Component Events](ComponentEvents) except that we will facilitate communication between components and pages using [Blazor.EventAggregrator](https://github.com/mikoskinen/Blazor.EventAggregator)  library.

  * [Localization](Localization)

    This sample shows how to use localization and perform language switching in Blazor server using a global resource.

  * [Localization using PO files](Localization-2)

    This sample shows how to use localization and perform language switching in Blazor server using Portable Object(PO) files.

  * [Localization using PO with multiple files](Localization-3)

    This sample is similar to the previous one above except this time we allow translation files to be split into multiple files per language. This allows better organization of language translation.

  * [Localization RTL/LTR support](Localization-4)

    This sample shows how to implement LTR/RTL support in Blazor.

  * [RenderTreeBuilder](RenderTreeBuilder)

    `RenderTreeBuilder` shows a Blazor (server) component implemented without Razor syntax.


dotnet6
# Server Size Blazor (7)

This is an amazing piece of technology where your interactive web UI is handled via C# and streamed back and forth using web socket via SignalR.

The source code to Razor Component is [here](https://github.com/aspnet/AspNetCore/tree/master/src/Components).

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
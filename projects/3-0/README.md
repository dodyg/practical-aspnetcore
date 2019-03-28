# .NET Core 3.0 Preview 3 (14)

All the samples here rely on ASP.NET Core 3.0 Preview 3. Make sure you download the SDK [here](https://dotnet.microsoft.com/download/dotnet-core/3.0).

The official migration guide from 2.2 to 3.0 is [here](https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-2.2&tabs=visual-studio)


* [Hello World](/projects/3-0/hello-world-with-reload)

  The classic hello world. Here are the important changes:
  
  * You can see how barebone the project file is. 
  * In your `program.cs` you need to add `using Microsoft.Extensions.Hosting;`.
  * JSON.NET has been removed from the Core shared framework and it needs to be added in if you need it.
  * Instead of `IHostingEnvironment`, use `IHostEnvironment`.
  * Instead of `EnvironmentName`, use `Environments`.
  * You can only inject `IConfiguration` and `IHostEnvironment` at `Startup` constructor.
  * ASP.NET Core 3 uses `GenericHost` instead of `WebHost`. So it is now
    ```
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().
                    UseEnvironment("Development");
                });
    }
    ```    

    instead of this in 2.2

    ```
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
    ```

* [Generic Hosting](/projects/3-0/hosting)

  You will find here how to setup things that you used to do in the previous versions of ASP.NET Core. In this sample we also forgo the use of `Startup` class.

  We also start examining the components of the Generic Hosting in the context of setting up your web app. The readme in this link contains more information and discussion.  

* [Integrate Newtonsoft.Json back](/projects/3-0/newtonsoft-json)

  ASP.NET Core has a new built in JSON Serializer/Deserializer. This sample shows how to integrate Newtonsoft.Json back to your project.

* [New Routing - Razor Page](/projects/3-0/new-routing)

  ASP.NET Core 3 allows more control on how to organize your endpoints using `app.UseRouting`. In this example, we just enable Razor Pages routes and nothing else.

* [New Routing - MVC](/projects/3-0/new-routing-2)

  ASP.NET Core 3 allows more control on how to organize your endpoints using `app.UseRouting`. In this example, we just enable MVC routes and nothing else.

* [New Routing - MVC with default route](/projects/3-0/new-routing-3)

  Map MVC routes with default `{controller=Home}/{action=Index}/{id?}` set up.

* [New Routing - RequestDelegate](/projects/3-0/new-routing-4)

  This example shows how to use `RequestDelegate` directly in `app.UseRouting` for `GET` operation using `MapGet`. `MapPost`, `MapPut`, and `MapDelete` are also available for use.

  This allow the creation of very minimalistic web services apps.


## Razor Component

This is an amazing piece of technology where your interactive web UI is handled via C# and streamed back and forth using web socket via SignalR.

The source code to Razor Component is [here](https://github.com/aspnet/AspNetCore/tree/master/src/Components).

All the samples in this section runs on SSL. If you have not gotten your local self-sign SSL in order yet, please read this [instruction](https://www.hanselman.com/blog/DevelopingLocallyWithASPNETCoreUnderHTTPSSSLAndSelfSignedCerts.aspx).

  * [Hello World](/projects/3-0/razor-component/HelloWorld)

    This is the simplest Razor component app you can create. It will show you clearly the building block of a Razor component application.

    There are two extra settings for dotnet watch to monitor `*.cshtml` and `*.razor` file changes on two projects to make your development experience better.

    ``` xml
        <Watch Include="**\*.cshtml" />
        <Watch Include="**\*.razor" />
    ```

  * [Rss Reader](/projects/3-0/razor-component/RssReader)

    This sample demonstrates that you can use normal server side packages with your Razor Component as it is a truly server side system. This sample uses `Microsoft.SyndicationFeed.ReaderWriter` package to parse an external RSS feed and display it.

  * [Js Integration](/projects/3-0/razor-component/JsIntegration)

    This sample shows how to access JavaScript functions available at `windows` global scope.

  * [Dependency Injection](/projects/3-0/razor-component/DependencyInjection)

    This sample shows you that the 'Transient' and 'Scoped' Dependency Injection method have different practical impact on Razor Component.

  * [SignalR Broadcast](/projects/3-0/razor-component/ChatR)

    This sample shows how to integrate SignalR with your Razor Component app.

  * [Layout](/projects/3-0/razor-component/Layout)

    This sample shows how to use layout and nested layouts.

  * [Multi Apps](/projects/3-0/razor-component/MultiApps)

    This sample shows how to host multiple Razor Component apps in a single server.
# .NET Core 3.0 Preview 3 (10)

All the samples here rely on ASP.NET Core 3.0 Preview 4. Make sure you download the SDK [here](https://dotnet.microsoft.com/download/dotnet-core/3.0).

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

  ASP.NET Core 3 allows more control on how to organize your endpoints using `app.UseEndpoints`. In this example, we just enable Razor Pages routes and nothing else.

* [New Routing - MVC](/projects/3-0/new-routing-2)

  ASP.NET Core 3 allows more control on how to organize your endpoints using `app.UseEndpoints`. In this example, we just enable MVC routes and nothing else.

* [New Routing - MVC with default route](/projects/3-0/new-routing-3)

  Map MVC routes with default `{controller=Home}/{action=Index}/{id?}` set up.

* [New Routing - RequestDelegate](/projects/3-0/new-routing-4)

  This example shows how to use `RequestDelegate` directly in `app.UseEndpoints` for `GET` operation using `MapGet`. `MapPost`, `MapPut`, and `MapDelete` are also available for use.

  This allow the creation of very minimalistic web services apps.

* [New Routing - RequestDelegate](/projects/3-0/new-routing-5)

  This example shows how to use `RequestDelegate` directly in `app.UseEndpoints` using `Map`.

* [New Routing - Interrogate available endpoints](/projects/3-0/new-routing-6)

  This example shows how to list all available endpoints in your app.

* [New Routing - RequestDelegate with HTTP verb filter](/projects/3-0/new-routing-7)

  This example shows how to use `RequestDelegate` directly in `app.UseEndpoints` using `MapMethods` that filter request based on one or more HTTP verbs.

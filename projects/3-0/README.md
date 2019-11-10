# ASP.NET Core 3.0 (57)

All the samples here rely on ASP.NET Core 3.0. Make sure you download the SDK [here](https://dotnet.microsoft.com/download/dotnet-core/3.0).

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

* [Endpoint Routing - Razor Page](/projects/3-0/new-routing)

  ASP.NET Core 3 allows more control on how to organize your endpoints using `app.UseEndpoints`. In this example, we just map Razor Pages routes and nothing else.

* [Endpoint Routing - MVC](/projects/3-0/new-routing-2)

  ASP.NET Core 3 allows more control on how to organize your endpoints using `app.UseEndpoints`. In this example, we just map MVC routes (attribute routing only, not convention routing) and nothing else.

* [Endpoint Routing - MVC with default route](/projects/3-0/new-routing-3)

  Map MVC routes with default `{controller=Home}/{action=Index}/{id?}` set up.

* [Endpoint Routing - RequestDelegate](/projects/3-0/new-routing-4)

  This example shows how to use `RequestDelegate` directly in `app.UseEndpoints` for `GET` operation using `MapGet`. `MapPost`, `MapPut`, and `MapDelete` are also available for use.

  This allow the creation of very minimalistic web services apps.

* [Endpoint Routing - RequestDelegate](/projects/3-0/new-routing-5)

  This example shows how to use `RequestDelegate` directly in `app.UseEndpoints` using `Map`.

* [Endpoint Routing - Interrogate available endpoints](/projects/3-0/new-routing-6)

  This example shows how to list all available endpoints in your app.

* [Endpoint Routing - RequestDelegate with HTTP verb filter](/projects/3-0/new-routing-7)

  This example shows how to use `RequestDelegate` directly in `app.UseEndpoints` using `MapMethods` that filter request based on one or more HTTP verbs.

* [Endpoint Routing - Static file fallback](/projects/3-0/new-routing-8)

  Return a static page when your request does not match anything else using `MapFallbackToFile`.

* [Endpoint Routing - Razor Page fallback](/projects/3-0/new-routing-9)

  Return a Razor Page when your request does not match anything else using `MapFallbackToPage`.

* [Endpoint Routing - Obtaining an Endpoint from your Middleware](/projects/3-0/new-routing-10)

  Use the brand new `HttpContext.GetEndPoint` extension method to examine the current endpoint that is being executed.

* [Endpoint Routing - How to obtain metadata in an Endpoint from a Razor page](/projects/3-0/new-routing-11)

  Use the brand new `EndPoint.Metadata.GetMetadata<>()` to get values from attributes at your Razor Page.

* [Endpoint Routing - Obtaining an Endpoint metadata from your Razor Page depending on the request method](/projects/3-0/new-routing-12)

  Unlike in MVC, you can't use `Attribute` from the method of a Razor Page. You can only use it from the Model class. This makes getting obtaining the appropriate metadata for each request require an extra step.

* [Endpoint Routing - Obtaining an Endpoint metadata from your MVC Controller](/projects/3-0/new-routing-13)

  Obtain Endpoint metadata from MVC Controller's Action methods.

* [Endpoint Routing - Obtaining Endpoint feature via IEndpointFeature](/projects/3-0/new-routing-14)

  Use `HttpContext.Features.Get<IEndpointFeature>();` to obtain `Endpoint` information for a given Middleware. You can accomplish the same thing using `HttpContext.GetEndpoint`.

* [Endpoint Routing - Attaching Metadata information to your inline Middleware](/projects/3-0/new-routing-15)

  Use `IEndpointConventionBuilder.WithMetadata` to attach metadata information to your inline Middleware.

* [Endpoint Routing - Map Areas by Convention](/projects/3-0/new-routing-16)

  Use `IEndpointRouteBuilder.MapAreaControllerRoute` to configure routing for your areas.

* [Endpoint Routing - enable MVC but without Views support](/projects/3-0/new-routing-17)

  Use `services.AddControllers` to provide MVC without Views supports. Razor Pages is not available. Perfect for Web APIs.

* [Endpoint Routing - enable MVC but with Views support but without Razor Page](/projects/3-0/new-routing-18)

  Use `services.AddControllersWithViews();` to provide MVC with Views supports. Razor Page is not available. So this similar to the "classic" MVC configuration.

* [Endpoint Routing - enable Razor Pages with MVC API support](/projects/3-0/new-routing-19)

  Use `services.AddRazorPages()` add supports for Razor Pages and MVC API.

* [Endpoint Routing - Convention based Routing](/projects/3-0/new-routing-20)
  
  Use `IEndpointRouteBuilder.MapControllerRoute` to configure convention based routing.

* [Endpoint Routing - A new way to map health check](/projects/3-0/new-routing-21)
  
  Use `IEndpointRouteBuilder.MapHealthChecks` to configure health check instead of `IApplicationBuilder.UseHealthChecks`.

* [Endpoint Routing - Configure Endpoints default on Kestrel](/projects/3-0/new-routing-22)

  We configure `KestrelServerOptions.ConfigureEndpointDefaults` so the Endpoints will run only on HTTP/2.

* [Endpoint Routing - Host Matching](/projects/3-0/new-routing-23)

  This example demonstrates on how to configure your endpoint to respond to a request from a specific host. In this example, GET `/` returns a different result depending whether you access it from `localhost:8111` and `localhost:8112`.

* [Endpoint Routing - Host Matching 2](/projects/3-0/new-routing-24)

  This produces the same exact effect as the [previous example above - Host Matching](/projects/3-0/new-routing-23) except that here we use `IEndpointConventionBuilder.WithMetadata` and `HostAttribute` instead of `IEndpointConventionBuilder.RequireHost`.

* [Endpoint Routing - Handle MVC routing dynamically](/projects/3-0/new-routing-25)

  This example shows how to handle MVC routing dynamically using `MapDynamicControllerRoute` and `DynamicRouteValueTransformer`.

* [Endpoint Routing - Handle Razor Pages routing dynamically](/projects/3-0/new-routing-26)

  This example shows how to handle Razor Pages routing dynamically using `MapDynamicPageRoute` and `DynamicRouteValueTransformer`.
  
* [Endpoint Routing - Setup to Razor Pages areas](/projects/3-0/new-routing-28)

  This example shows how to create Razor Pages areas.

* [Endpoint Routing - Map a route to a Razor Pages in an area](/projects/3-0/new-routing-27)

  Map a route to a Razor Pages located in an Area using `Conventions.AddAreaPageRoute`. 

* [Endpoint routing - setup a fallback page in a Razor Pages area](/projects/3-0/new-routing-29)

  This sample shows you how to setup a fallback page located in a Razor Pages area. 

* [Endpoint routing - serve different fallback pages depending on route pattern match](/projects/3-0/new-routing-30)

  This sample shows how to return different fallback page located in areas depending on the route pattern that matches the request.
  

## Razor View

* [Markup at @functions](/projects/3-0/razor)
   
  Now you can use markup inside methods at `@functions` block.

* [Markup at code block](/projects/3-0/razor-2)
   
  Now you can use markup inside functions at code block.

## GRPC

* [Unary - Hello World](/projects/3-0/grpc)

  This sample shows a simple request/response gRPC call.

* [Server Streaming - Message Server](/projects/3-0/grpc-2)

  This sample shows how to do simple gRPC sever streaming.

* [Client Streaming - Fortune Cookie Server](/projects/3-0/grpc-3)

  This sample shows how to do simple gRPC client streaming.

* [Bidirectional Streaming - Forever Ping Pong](/projects/3-0/grpc-4)

  This sample shows how to do simple gRPC client/server bidirectional streaming.

* [Data format - string, enum and datetime](/projects/3-0/grpc-5)

  This sample shows how to define Protocol Buffers format to support sending enum, string and datetime.

* [Data format - nested types and repeated values (list)](/projects/3-0/grpc-6)

  This sample shows how to define Protocol Buffers format to support sending nested types and repeated values (list).

* [Data format - map values (dictionary)](/projects/3-0/grpc-7)

  This sample shows how to define Protocol Buffers format to support sending map values (dictionary).

* [Data format - oneof](/projects/3-0/grpc-8)

  This sample demonstrates how to use `oneof` type to allow you to check whether the value of a property is set or not, essentialy emulating nullable type.

* [Server Streaming - Kitty Server](/projects/3-0/grpc-9)

  This sample shows how to stream an image in chunks from server to client.

## Json

All about the new `System.Text.Json` namespace.

* [Json](/projects/3-0/json)

  Use `JsonSerializer.SerializeAsync` to serializer your object to JSON directly to stream.

* [Json - Options](/projects/3-0/json-2)

  Use `JsonSerializerOptions` to control certain aspect of your object serialization.

* [Json - Serializing Anonymous Type](/projects/3-0/json-3)

  Create adhoc JSON document using anonymous type and serialize it to stream directly using `JsonSerializer.SerializeAsync`.

* [Json - Control serialization using attributes](/projects/3-0/json-4)

  Use `[JsonPropertyName]` and `[JsonIgnore]` to control JSON serialization output.

* [Json - Serialize a Dictionary of object](/projects/3-0/json-5)

  A `Dictionary<string, object>` can be used to generate pretty much any shape of JSON document that you want.

* [Json - Write a JSON document to the stream directly](/projects/3-0/json-6)

  Use `Utf8JsonWriter` to write a JSON document directly to a stream.

* [Json - Implement a custom naming policy](/projects/3-0/json-7)

  Create a custom naming policy that generate JSON property names in snake_case. 

* [Json - Benchmark](/projects/3-0/json-8)

  Benchmark on two different approaches of generating snake_case property names. 

* [Json - Custom Converter](/projects/3-0/json-9)

  Implement a custom type converter. In this example we convert `TimeSpan`.

## Misc

* [Version info](/projects/3-0/version)
 
  Show various version info of the framework your system is running on.

* [IHttpResponseBodyFeature](/projects/3-0/features-http-body-response)

  This new Feature interface consolidate previous version's three response body APIs into one

* [Trailing headers](/projects/3-0/trailing-headers)

  This example shows how to issue trailing HTTP headers. Normal HTTP headers must be issued before body of the HTTP Response starts being written. Trailing headers allows you to issue headers after the HTTP body has been written. 
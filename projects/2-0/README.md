# ASP.NET Core 2.0

Note that all the code here will run on ASP.NET Core 2.0 however these samples uses [2.1.700](https://dotnet.microsoft.com/download/dotnet-core/2.1).

## What's new in ASP.NET Core 2.0 (11)

  This section will show new things in [ASP.NET Core 2.0](https://github.com/aspnet/Home/releases/tag/2.0.0). This is a good explanation on [what's new on ASP.NET Core 2.0](https://blogs.msdn.microsoft.com/webdev/2017/08/25/asp-net-core-2-0-features-1/).

  * [A new way of configuring logging](logging)

    Now you configure logging at `Program` instead of `Startup.Configure` via `ConfigureLogging`. 

  * [Logging filtering](logging-with-filter)

    Now you can adjust what kind of logging information from various part of ASP.NET Core and your app you want show/stored.

  * [IConfiguration is now core](iconfiguration)

    ASP.NET Core 1.1

    ```
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure
            app.Run(context =>
            {
                return context.Response.WriteAsync('hello world');
            });
        }
    ```

    ASP.NET Core 2.0

    ```
      public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
      {
          //These are the four default services available at Configure
          app.Run(context =>
          {
              return context.Response.WriteAsync(configuration["greeting"]);
          });
      }
    ```

  * [Session Feature with Redis using JSON Serialization](features-session-redis-2)

    This shows how to use session with ```Redis``` store using Json instead of ```BinaryFormatter```.

    Make sure you have ```Redis``` running on your ```localhost``` at default port. The connection string is specified at ```appsetings.json```.

  * [Anti Forgery on Form](anti-forgery)

    This exists on since .NET Core 1.0 however the configuration for the cookie has changed slightly. We are using ```IAntiForgery``` interface to store and generate anti forgery token to prevent XSRF/CSRF attacks. 

  * [Razor Pages Basic](razor-pages-basic)

    This is the simplest example of the brand new `Razor Pages`. It shows the two approaches to `Razor Pages`, one with inline code behind and another with separate code behind.

  * [Razor Pages and MVC Basic](razor-pages-mvc)

    Compare and contrast on how the same task can be performed by using `Razor Pages` and `MVC`.

    This sample also shows you how to us `Entity Framework Core` In-Memory Database.

  * [UseRouter extension](use-router)

    Use ```app.UseRouter()``` extension to create minimalistic HTTP services similar to Nancy.

  * [UseRouter extension 2](use-router-2)

    Use ```app.UseRouter()``` with alternative lambda signature.

  * [IHostedService](ihosted-service)

    Implement background tasks using the new `IHostedService` interface.
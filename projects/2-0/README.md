# ASP.NET Core 2.0

Note that all the code here will run on ASP.NET Core 2.0 however these samples uses [2.1.700](https://dotnet.microsoft.com/download/dotnet-core/2.1).

## What's new in ASP.NET Core 2.0 (11)

  This section will show new things in [ASP.NET Core 2.0](https://github.com/aspnet/Home/releases/tag/2.0.0). This is a good explanation on [what's new on ASP.NET Core 2.0](https://blogs.msdn.microsoft.com/webdev/2017/08/25/asp-net-core-2-0-features-1/).

  * [Hello World with Microsoft.AspNetCore.All package](hello-world-startup-all-package)

      If you are targeting `netcoreapp2.0`, you can use `Microsoft.AspNetCore.All` meta package that download **most** of the necessary packages to develop an ASP.NET Core/MVC system (including EF DB support).

      It also adds the following packages

      ```
      Installing Microsoft.IdentityModel.Logging 1.1.4.
      Installing Microsoft.IdentityModel.Tokens 5.1.4.
      Installing runtime.win-x64.runtime.native.System.Data.SqlClient.sni 4.4.0.
      Installing runtime.win-x86.runtime.native.System.Data.SqlClient.sni 4.4.0.
      Installing runtime.win-arm64.runtime.native.System.Data.SqlClient.sni 4.4.0.
      Installing System.IdentityModel.Tokens.Jwt 5.1.4.
      Installing System.Text.Encoding.CodePages 4.4.0.
      Installing runtime.native.System.Data.SqlClient.sni 4.4.0.
      Installing Microsoft.Azure.KeyVault.WebKey 2.0.7.
      Installing Microsoft.Rest.ClientRuntime.Azure 3.3.7.
      Installing Microsoft.Rest.ClientRuntime 2.3.8.
      Installing SQLitePCLRaw.lib.e_sqlite3.v110_xp 1.1.7.
      Installing SQLitePCLRaw.lib.e_sqlite3.linux 1.1.7.
      Installing SQLitePCLRaw.lib.e_sqlite3.osx 1.1.7.
      Installing SQLitePCLRaw.provider.e_sqlite3.netstandard11 1.1.7.
      Installing Microsoft.IdentityModel.Protocols 2.1.4.
      Installing Microsoft.NETCore.App 2.0.0-preview2-25407-01.
      Installing Microsoft.NETCore.DotNetHostPolicy 2.0.0-preview2-25407-01.
      Installing Microsoft.NETCore.Platforms 2.0.0-preview2-25405-01.
      Installing NETStandard.Library 2.0.0-preview2-25401-01.
      Installing Microsoft.NETCore.DotNetHostResolver 2.0.0-preview2-25407-01.
      Installing Microsoft.Packaging.Tools 1.0.0-preview2-25401-01.
      Installing System.Interactive.Async 3.1.1.
      Installing SQLitePCLRaw.core 1.1.7.
      Installing Microsoft.IdentityModel.Protocols.OpenIdConnect 2.1.4.
      Installing SQLitePCLRaw.bundle_green 1.1.7.
      Installing Microsoft.Azure.KeyVault 2.3.2.
      Installing Microsoft.IdentityModel.Clients.ActiveDirectory 3.14.1.
      Installing WindowsAzure.Storage 8.1.4.
      Installing System.Data.SqlClient 4.4.0.
      Installing Microsoft.NETCore.DotNetAppHost 2.0.0-preview2-25407-01.
      ```

      In ASP.NET Core 2.0, this is the recommended way to start your host

      ```
      public class Program
      {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development")
                .Build();
      }
      ```

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

  * [Session Feature with Redis using BinaryFormatter](features-session-redis)

    This shows how to use session with ```Redis``` store. We use ```BinaryFormatter``` which is only available at `.NET Core 2.0` or above to serialize and deserialize your object. *The better way is to serialize your object using Json - BinaryFormatter is SLOW*.

    Make sure you have ```Redis``` running on your ```localhost``` at default port. The connection string is specified at ```appsetings.json```.

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
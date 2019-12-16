# ASP.NET COre 2.1


## What's new in ASP.NET Core 2.1(6)

  *Pre-requisite*: Make sure you download .NET Core SDK [2.1.700](https://dotnet.microsoft.com/download/dotnet-core/2.1) otherwise below examples won't work.

  **New code based idiom to start your host for ASP.NET Core 2.1**

  It is recommended to use the following approach 

  ```CSharp
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
  ```

  instead of

  ```CSharp
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

  * [Hello World with Microsoft.AspNetCore.App package](hello-world-startup-app-package)

    If you are targeting `netcoreapp3.1`, you can use `Microsoft.AspNetCore.App` meta package that download **most** of the necessary packages to develop an ASP.NET Core/MVC system (including EF DB support).

    This package is a trimmed version of `Microsoft.AspNetCore.All` meta package. You can find more details about the removed dependencies [here](https://github.com/aspnet/Announcements/issues/287).

    `Microsoft.AspNetCore.App` is going to be the default meta package when you create a new ASP.NET Core 2.1 package.



  * [Supress Status Messages](suppress-status-messages)

    You can hide status messages when you start up your web application. It's a small useful thing.

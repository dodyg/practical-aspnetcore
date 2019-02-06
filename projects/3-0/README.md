# .NET Core 3.0 Preview 2 (12)

All the samples here rely on ASP.NET Core 3.0 Preview 2. Make sure you download the SDK [here](https://blogs.msdn.microsoft.com/webdev/2019/01/29/aspnet-core-3-preview-2/).

Migration path from 2.2 to 3.0 is [here](https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-2.2&tabs=visual-studio)


* [Hello World](/projects/3-0/hello-world-with-reload)

  The classic hello world. You can see how barebone the project file is. ASP.NET Core 3 uses `GenericHost` instead of `WebHost`.

  In your `program.cs` you will also need to add `using Microsoft.Extensions.Hosting;`.

  So it is now
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

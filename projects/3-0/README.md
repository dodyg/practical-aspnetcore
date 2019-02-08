# .NET Core 3.0 Preview 2 (2)

All the samples here rely on ASP.NET Core 3.0 Preview 2. Make sure you download the SDK [here](https://blogs.msdn.microsoft.com/webdev/2019/01/29/aspnet-core-3-preview-2/).

The official migration guide from 2.2 to 3.0 is [here](https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-2.2&tabs=visual-studio)


* [Hello World](/projects/3-0/hello-world-with-reload)

  The classic hello world. Here are the important changes:
  
  * You can see how barebone the project file is. 
  * In your `program.cs` you need to add `using Microsoft.Extensions.Hosting;`.
  * JSON.NET has been removed from the Core shared framework and it needs to be added in if you need it.
  * You can only inject `IConfiguration` and `IHostingEnvironment` at `Startup` constructor.
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

  You will find here how to setup things that you used to do in the previous versions of ASP.NET Core.

  We also start examining the components of the Generic Hosting in the context of setting up your web app. The readme in this link contains more information and discussion.  
  
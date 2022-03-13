# Hello World using the new simplified hosting

This hello world sample uses the brand new `WebApplication` hosting class. This simplifies configuring an ASP.NET Core application by a mile.


This is how this code sample used to look like using `IHostBuilder`. 
``` C#
namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                // Duplicate the code below and write more messages. Save and refresh your browser to see the result.
                await context.Response.WriteAsync("Hello world .NET 6. Make sure you run this app using 'dotnet watch run'.");
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}
```

and now 

``` c#
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

WebApplication app = WebApplication.Create();

app.Run(async context =>
{
    // Duplicate the code below and write more messages. Save and refresh your browser to see the result.
    await context.Response.WriteAsync("Hello world .NET 6. Make sure you run this app using 'dotnet watch run'.");
});

await app.RunAsync();
```

You can read the implementation of ```WebApplication``` [here](https://github.com/dotnet/aspnetcore/blob/main/src/DefaultBuilder/src/WebApplication.cs). 
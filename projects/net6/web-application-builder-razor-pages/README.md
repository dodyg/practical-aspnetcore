# WebApplicationBuilder - Razor Pages

In most cases using ```WebApplication``` isn't enough because you need to configure additional services to be used in your system. This is where ```WebApplicationBuilder``` comes. It allows you to configure services and other properties.

This example shows how to enable Razor Pages using the minimal hosting approach.

``` csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder();
builder.Services.AddRazorPages();

var app = builder.Build();
app.MapRazorPages();

await app.RunAsync();
```

In contrast this is how it is done using [Startup.cs](/projects/razor-pages/hello-world/src/Program.cs) (not all codes included). 

``` csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
        });
    }
}
```    


You can read the implementation of ```WebApplication``` [here](https://github.com/dotnet/aspnetcore/blob/main/src/DefaultBuilder/src/WebApplication.cs) and its sibling ```WebApplicationBuilder``` [here](https://github.com/dotnet/aspnetcore/blob/main/src/DefaultBuilder/src/WebApplicationBuilder.cs)




## MVC and Pages Routing

  We are exploring every single boring details about MVC and Pages routing in this section.

  * [MVC Routing - 1](/projects/mvc/routing-1)

    Demonstrates fixed routing and default conventional routing for ASP.NET MVC.
  
  * [MVC Routing - 2](/projects/mvc/routing-2)

    Similar to `Routing - 1` but using alternative method to specify default conventional routing.

  * [MVC Routing - 3](/projects/mvc/routing-3)

    Use `UseMvcWithDefaultRoute` so you don't have to define the default conventional route. This is how the extension is [implemented](https://github.com/aspnet/Mvc/blob/e2de54a92d8254a27f9eefd77e08370c7b17fa5d/src/Microsoft.AspNetCore.Mvc.Core/Builder/MvcApplicationBuilderExtensions.cs).

    ``` csharp
    public static IApplicationBuilder UseMvcWithDefaultRoute(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        return app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
        });
    }
    ```

  * [MVC Routing - 4](/projects/mvc/routing-4)

    The simplest example for attribute routing. We use the `Route` attribute at the Controller. This only allows you to have one Action per Controller.

  * [MVC Routing - 5](/projects/mvc/routing-5)

    We use the `Route` attribute at Action methods (in contrast to previous example). This allows you to have multiple Actions in a Controller.

  * [MVC Routing - 6](/projects/mvc/routing-6)

    Demonstrate the usage of `HttpGet` and `HttpPost`.

  * [MVC Routing - 7](/projects/mvc/routing-7)

    Demonstrate the usage of `[controller]` replacement token at the `Route` attribute.
  
  * [MVC Routing - 8](/projects/mvc/routing-8)

    Demonstrate the usage of `[controller]` and `[action]` replacement tokens at the `Route` attribute.

  * [MVC Routing - 9](/projects/mvc/routing-9)

    Demonstrate the usage of `[action]` replacement tokens at the `HttpGet` attribute.

  * [MVC Routing - 10](/projects/mvc/routing-10)

    Demonstrate the usage of `IActionConstraint` attribute.

    The following map routing will search all HomeController.About action accross the assembly regardless of namespace. If you have multiple
    HomeController.About, it will generate error because the framework cannot decide which method to use. This sample demonstrates on how using
    a custom `IActionConstraint` attribute solves this problem.

    ``` csharp
    app.UseMvc(routes =>
    {
        routes.MapRoute(
        "About",
        "{id}/About",
        defaults: new { controller = "Home", Action = "About" });
    });
    ```

  * [Routing Table](/projects/mvc/routing-table)

    Use `Microsoft.AspNetCore.Mvc.Infrastructure.IActionDescriptorCollectionProvider` to interrogate the routing table to display all the registered routes in the system, whether it is using conventional routing or attribute routing.


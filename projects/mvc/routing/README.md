# MVC Routing (9)

  We are exploring every single boring details about MVC routing in this section.

  * [MVC Routing - 1](/projects/mvc/routing/routing-1)

    Demonstrates fixed routing and default conventional routing for ASP.NET MVC.
  
  * [MVC Routing - 2](/projects/mvc/routing/routing-2)

    Use `endpoints.MapDefaultControllerRoute();` to specify default convention routing for ASP.NET MVC.

  * [MVC Routing - 3](/projects/mvc/routing/routing-3)

    The simplest example for attribute routing. We use the `Route` attribute at the Controller. This only allows you to have one Action per Controller.

  * [MVC Routing - 4](/projects/mvc/routing/routing-4)

    We use the `Route` attribute at Action methods (in contrast to previous example). This allows you to have multiple Actions in a Controller.

  * [MVC Routing - 5](/projects/mvc/routing/routing-5)

    Demonstrate the usage of `HttpGet` and `HttpPost`.

  * [MVC Routing - 6](/projects/mvc/routing/routing-6)

    Demonstrate the usage of `[controller]` replacement token at the `Route` attribute.
  
  * [MVC Routing - 7](/projects/mvc/routing/routing-7)

    Demonstrate the usage of `[controller]` and `[action]` replacement tokens at the `Route` attribute.

  * [MVC Routing - 8](/projects/mvc/routing/routing-8)

    Demonstrate the usage of `[action]` replacement tokens at the `HttpGet` attribute.

  * [MVC Routing - 9](/projects/mvc/routing/routing-9)

    Demonstrate the usage of `IActionConstraint` attribute.

    The following map routing will search all HomeController.About action accross the assembly regardless of namespace. If you have multiple
    HomeController.About, it will generate error because the framework cannot decide which method to use. This sample demonstrates on how using
    a custom `IActionConstraint` attribute solves this problem.


  dotnet6
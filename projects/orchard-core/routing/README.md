# Routing

This sample shows how the routing works in an Orchard Core Framework application.

We have two modules in this sample:

* ForumModule
* TicketModule

Each of this module uses `OrchardCore.Module.Targets`.

The host application is a normal ASP.NET Core app that uses `OrchardCore.Application.Mvc.Targets` and has references to the projects of `ForumModule` and `TicketModule`.

By default, OCF creates areas based on the name of your modules. Hence we have `ForumModule` and `TicketModule` areas in this app.

If you want to customize the routing of each module, you can do it via the `Startup.cs` located under each module. Make sure that `routes.MapAreaControllerRoute` `AreaName` matches the name of your module otherwise it won't work.
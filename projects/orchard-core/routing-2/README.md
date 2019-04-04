# Routing with Razor Pages

This sample shows how the routing works in an Orchard Core Framework application when you are using Razor Pages. *Note*: To see the where the custom routes are defined, go to individual page at the modules. It is defined using `@page` attribute at each page.

We have two modules in this sample:

* ForumModule
* TicketModule

Each of this module uses `OrchardCore.Module.Targets`.

The host application is a normal ASP.NET Core app that uses `OrchardCore.Application.Mvc.Targets` and has references to the projects of `ForumModule` and `TicketModule`.

By default, OCF creates areas based on the name of your modules. Hence we have `ForumModule` and `TicketModule` areas in this app.

# Orchard Core Framework (4)


This section contains samples of project relying on [Orchard Core Framework](https://orchardcore.readthedocs.io/en/dev/). This section is a bit more advanced than the other parts of Practical ASP.NET Core.

Orchard Core Framework is interesting because it provides infrastructure to modularize your application. It also go further in providing facilities to create multi tenant application, which is super hard to get right if you start from scratch.

Orchard Core Framework powers Orchard Core CMS - you can use the framework independently on systems that have nothing to do with content management.

Notes:

- To run a sample, go to each sample folder and go to the `Host` folder.
- All module will have `Module` prefix.
- We will be using RC1 (or later) version of Orchard Core.
- The samples in this section will receive frequent revisions as I am exploring the framework as I produce these samples.

* [Routing - MVC](/projects/orchard-core/routing)

  This sample shows how routing works in an Orchard Core Framework app.

* [Routing - Razor Pages](/projects/orchard-core/routing-2)

  This sample shows how routing works in an Orchard Core Framework app when you are using Razor Pages.

* [Static Files](/projects/orchard-core/static-files)

  This sample shows how to use static files in the module.

* [Multi Tenant](/projects/orchard-core/multi-tenant)

  This sample shows how Orchard Core Framework handles multi tenancy and how each tenant have its own configuration file.


dotnet6
#static files

Each Orchard Core module can have its own static file at `wwwroot`. It just works. You just have to make sure that your links points to the right area, which is your module name. In this sample, it is `ForumModule`.

The downside is that currently Orchard Core does not support `asp-append-version` which enables cache bursting. Hopefully this is going to be rectified soon.
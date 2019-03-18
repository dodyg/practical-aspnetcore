# Multi App

This is an example on how to host two different Razor Component apps in a single server. 

Pay attention to difference between [App1.cshtml](AppHost/Pages/App1.cshtml) and [App2.cshtml](AppHost/Pages/App2.cshtml).

`App1.cshtml` uses `<script src="_framework/components.server.js"></script>` and `App2.cshtml` uses `<script src="/app2.components.server.js"></script>`.

The one that `App2.cshtml` uses is a hack.

Run it by using `dotnet watch run` on AppHost folder.

Note: I can only make this project thanks to the hack shown at this [sample](https://github.com/jbomhold3/workingsplitproject). There are probably better ways to achieve hosting multiple Razor Component apps. I will update the sample once I found one. 
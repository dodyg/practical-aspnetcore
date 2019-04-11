# SignalR broadcast

This sample attempts to demonstrate on how to use SignalR in your own Razor Component app. You will also see the usage of `ComponentBase.Invoke` in the codebase when updating the UI after receiving data from SignalR.

Note: apparently ASP.NET Core team is investigating a way to allow app developer to reuse the SignalR connection that Razor Component has already established (in this sample we have to create a 2nd SignalR). Stay tuned.
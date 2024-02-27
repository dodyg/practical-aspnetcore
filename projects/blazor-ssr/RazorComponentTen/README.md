# Razor Component multi render

This sample shows the ability for Blazor to host components with different rendering mode in a single Razor Component Page.

The Razor Component Page is server side render. It handles a POST form,  hosts "number" component with with Streaming rendering and hosts "interactive" components backed by Blazor Web Assembly and Blazor Server.

The Web Assembly components **need to be hosted in a separate project**. 
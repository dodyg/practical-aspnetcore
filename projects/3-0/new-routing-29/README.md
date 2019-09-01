# New Routing - Fallback page for Razor Pages areas

This sample shows you how to setup a fallback page for Razor Pages areas. This means that you can have more than one areas (such as this example). When the router cannot match any given non file route, it will resort to returning the fallback page.

You can only have one `endpoints.MapFallbackToAreaPage` definition in your system.
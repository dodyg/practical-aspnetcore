# New Routing - Fallback page for Razor Pages areas

This sample shows you how to setup a fallback page located in a Razor Page area. When the router cannot match any given non file route (e.g without extension), it will resort to returning the fallback page.

You can only have one `endpoints.MapFallbackToAreaPage` or `endpoints.MapFallbackToPage` or `endpoints.MapFallbackToFile` definition in your system. If you want to have multiple fallback pages, you need to use the ones with pattern.
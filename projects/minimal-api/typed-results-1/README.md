# TypedResults

> The new `Microsoft.AspNetCore.Http.TypedResults` static class is the “typed” equivalent of the existing `Microsoft.AspNetCore.Http.Results` class. You can use TypedResults in your minimal APIs to create instances of the in-framework IResult-implementing types and preserve the concrete type information.
>
> [Microsoft](https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-dotnet-7-preview-4/)

This allows you to check the full concrete type of the result of your minimal API method. Run `dotnet test` to see the result.


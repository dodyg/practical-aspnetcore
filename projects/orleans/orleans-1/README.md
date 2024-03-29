# Orleans  Hello World with ASP.NET Core

This sample shows how to use Orleans 8 with ASP.NET Core 8. 

The changes in Orleans 7 or above compared to previous version of Orleans.
- Simplified configuration of just linking to [Microsoft.Orleans.Server](https://www.nuget.org/packages/Microsoft.Orleans.Server).
- Using the new serializer using `GenerateSerializer` and `Id` attributes.
- Grain now implements `IGrain` instead of inheriting from `Grain` class (POCO grains).
- Removing calls for `ConfigureApplicationParts`.
- Using type alias 

> By default, Orleans will serialize your type by encoding its full name. You can override this by adding an Orleans.AliasAttribute. Doing so will result in your type being serialized using a name which is resistant to renaming the underlying class or moving it between assemblies. Type aliases are globally scoped and you cannot have two aliases with the same value in an application. For generic types, the alias value must include the number of generic parameters preceded by a backtick, for example, MyGenericType<T, U> could have the alias [Alias("mytype`2")].
>
> [What's new in Orleans](https://learn.microsoft.com/en-gb/dotnet/orleans/whats-new-in-orleans)
 

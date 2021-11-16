# C# 10

This example shows various features of C# 10 that is relevant for Web Development.


## Implicit using

[Find what namespaces automatically imported](https://docs.microsoft.com/en-us/dotnet/core/compatibility/sdk/6.0/implicit-namespaces-rc1) if you enable implicit using.

## Using with expression with anoymous type

```csharp
    var person = new { Name = "Dody", Nationality = "Indonesia " };
    var person2 = person with { Nationality = "Italy"};
```

## Natural lambda type

```csharp
    var result = IResult () => Results.Json(new Person("Dody", "Gunawinata"));
    Func<IResult> result2 = result;
```

## LINQ FirstOrDefault

```csharp
    var find = list.Where(x => x > 10).FirstOrDefault(-10);
```

## Assign and Initialize in the same tuple

```csharp
    Person GetPerson() => new Person("Dody", "Gunawinata");
    string firstName;
    (firstName, var lastName) = GetPerson();
```

## Record struct

```csharp
    public record struct Id(int Value);
```

## ArgumentNullException.ThrowIfNull

```csharp
    ArgumentNullException.ThrowIfNull(null);
```

## Process Path

```csharp
    Environment.ProcessPath
```

## PeriodicTimer

```csharp
    using PeriodicTimer timer = new (TimeSpan.FromSeconds(2));
    while(await timer.WaitForNextTickAsync())
        yield return DateTime.UtcNow;
```

## Random number generator

```csharp
    BitConverter.ToInt32(System.Security.Cryptography.RandomNumberGenerator.GetBytes(3000))
```

## CallerArgumentExpression

```csharp
    string SayHello(string name, [CallerArgumentExpression("name")] string exp = "")
        => @$"called ""{exp}"" parameter with value {name}";
```

## Constant interpolated string
```csharp
    const string MyName = "Dody Gunawinata";
    const string Profile = $"{MyName}";
```

## Extended property patterns
```csharp
    if (c is Club { Id.Value: 1} matched) 
        return matched.Name;
```
# Writable DOM - Create a JSON document using JsonObject

In this example we are demonstrating on how to construct a JSON document

Verbose version
``` csharp
var verbose = new JsonObject()
{
    ["name"] = JsonValue.Create("anne"),
    ["age"] = JsonValue.Create(34),
    ["gender"] = JsonValue.Create("female"),
    ["favoriteNumbers"] = new JsonArray()
    {
        JsonValue.Create(1),
        JsonValue.Create(2),
        JsonValue.Create(3)
    }
};
```

Terse version through implicit operator
``` csharp

var terse = new JsonObject()
{
    ["name"] = "anne",
    ["age"] = 34,
    ["gender"] = "female",
    ["favoriteNumbers"] = new JsonArray()
    {
        1, 2, 3
    }
};
```

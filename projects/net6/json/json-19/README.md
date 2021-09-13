# Writable DOM - Create a JSON document using JsonArray

In this example we are demonstrating on how to construct a JSON document using `JsonArray`.

Verbose version
``` csharp
var verbose = new JsonArray()
{
    new JsonObject()
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
    },
    new JsonObject()
    {
        ["name"] = JsonValue.Create("hadi"),
        ["age"] = JsonValue.Create(29),
        ["gender"] = JsonValue.Create("non-binary"),
        ["favoriteNumbers"] = new JsonArray()
        {
            JsonValue.Create(1),
            JsonValue.Create(5),
            JsonValue.Create(6)
        }
    },
    new JsonObject()
    {
        ["name"] = JsonValue.Create("abdelfattah"),
        ["age"] = JsonValue.Create(30),
        ["gender"] = JsonValue.Create("non-binary"),
        ["favoriteNumbers"] = new JsonArray()
        {
            JsonValue.Create(3),
            JsonValue.Create(9),
            JsonValue.Create(10),
            JsonValue.Create(11)
        }
    }
};
```

Terse version through implicit operator
``` csharp
var terse = new JsonArray()
{
    new JsonObject()
    {
        ["name"] = "anne",
        ["age"] = 34,
        ["gender"] = "female",
        ["favoriteNumbers"] = new JsonArray(1, 2, 3)
    },
    new JsonObject()
    {
        ["name"] = "hadi",
        ["age"] = 29,
        ["gender"] = "non-binary",
        ["favoriteNumbers"] = new JsonArray(1, 5, 6)
    },
    new JsonObject()
    {
        ["name"] = "abdelfattah",
        ["age"] = 30,
        ["gender"] = "non-binary",
        ["favoriteNumbers"] = new JsonArray(3, 9, 10, 11)
    }
};
```

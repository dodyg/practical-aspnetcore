# Writable DOM - Remove elements

This example shows how to update remove an object property and an element in an array.

```csharp
objectNode[0].AsObject().Remove("name");
objectNode.AsArray().RemoveAt(1);
```
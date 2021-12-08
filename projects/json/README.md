# Json (22)

All about the new `System.Text.Json` namespace.

* [Json](/projects/json/json)

  Use `JsonSerializer.SerializeAsync` to serializer your object to JSON directly to stream.

* [Json - Options](/projects/json/json-2)

  Use `JsonSerializerOptions` to control certain aspect of your object serialization.

* [Json - Serializing Anonymous Type](/projects/json/json-3)

  Create adhoc JSON document using anonymous type and serialize it to stream directly using `JsonSerializer.SerializeAsync`.

* [Json - Control serialization using attributes](/projects/json/json-4)

  Use `[JsonPropertyName]` and `[JsonIgnore]` to control JSON serialization output.

* [Json - Serialize a Dictionary of object](/projects/json/json-5)

  A `Dictionary<string, object>` can be used to generate pretty much any shape of JSON document that you want.

* [Json - Write a JSON document to the stream directly](/projects/json/json-6)

  Use `Utf8JsonWriter` to write a JSON document directly to a stream.

* [Json - Implement a custom naming policy](/projects/json/json-7)

  Create a custom naming policy that generate JSON property names in snake_case. 

* [Json - Benchmark](/projects/json/json-8)

  Benchmark on two different approaches of generating snake_case property names. 

* [Json - Custom Converter](/projects/json/json-9)

  Implement a custom type converter. In this example we convert `TimeSpan`.

* [Json - Control JsonIgnore behaviour](/projects/json/json-10)

  Demonstrate the three different ways you can control the behaviour of `[JsonIgnore]` per property.
  
* [Json - Custom Converter 2](/projects/json/json-11)

  Implement a custom type coverter for DateTime type for JsonSerializer in the format of JSON date ticks.


## Writable JSON DOM

   [Design document for the Writable JSON API](https://github.com/dotnet/designs/blob/main/accepted/2020/serializer/WriteableDomAndDynamic.md)

* [Primitives](/projects/json/json-12)
  
  This sample shows how to parse and access number, string and an array values from JSON string.

* [Object](/projects/json/json-13)

  This sample shows how to parse and access objects from JSON string. We will be using `JsonObject` as well.

* [Finding a node using LINQ](/projects/json/json-14)

  This sample shows how to find a node based on of its value using LINQ.

* [Finding a node using LINQ 2](/projects/json/json-15)

  This sample shows how to find a node based on two of its values (a string and an array) using LINQ.

* [Finding a node using LINQ 3](/projects/json/json-16)

  This sample shows how to find a node based of an absence of a property using LINQ.

* [Finding a node using LINQ 4](/projects/json/json-17)

  In this example we are trying to find a node in an array that has a specific value on its array property.

* [Construct a JSON document](/projects/json/json-18)

  This sample shows how to construct a JSON document using `JsonObject`.

* [Construct a JSON document](/projects/json/json-19)

  This sample shows how to construct a JSON document using `JsonArray`.

* [Update a JSON document](/projects/json/json-20)

  This sample shows how to update properties of a JSON document.

* [Delete elements in a JSON document](/projects/json/json-21)

  This example shows how to update remove an object property and an element in an array.

* [Add items into a JSON array](/projects/json/json-22)
  
  This example shows how to add items at the first position of an array and at the last position.

  dotnet6
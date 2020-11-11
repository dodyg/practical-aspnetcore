# Json (11)

All about the new `System.Text.Json` namespace.

* [Json](/projects/5-0/json/json)

  Use `JsonSerializer.SerializeAsync` to serializer your object to JSON directly to stream.

* [Json - Options](/projects/5-0/json/json-2)

  Use `JsonSerializerOptions` to control certain aspect of your object serialization.

* [Json - Serializing Anonymous Type](/projects/5-0/json/json-3)

  Create adhoc JSON document using anonymous type and serialize it to stream directly using `JsonSerializer.SerializeAsync`.

* [Json - Control serialization using attributes](/projects/5-0/json/json-4)

  Use `[JsonPropertyName]` and `[JsonIgnore]` to control JSON serialization output.

* [Json - Serialize a Dictionary of object](/projects/5-0/json/json-5)

  A `Dictionary<string, object>` can be used to generate pretty much any shape of JSON document that you want.

* [Json - Write a JSON document to the stream directly](/projects/5-0/json/json-6)

  Use `Utf8JsonWriter` to write a JSON document directly to a stream.

* [Json - Implement a custom naming policy](/projects/5-0/json/json-7)

  Create a custom naming policy that generate JSON property names in snake_case. 

* [Json - Benchmark](/projects/5-0/json/json-8)

  Benchmark on two different approaches of generating snake_case property names. 

* [Json - Custom Converter](/projects/5-0/json/json-9)

  Implement a custom type converter. In this example we convert `TimeSpan`.

* [Json - Control JsonIgnore behaviour](/projects/5-0/json/json-10)

  Demonstrate the three different ways you can control the behaviour of `[JsonIgnore]` per property.
  
* [Json - Custom Converter 2](/projects/5-0/json/json-11)

  Implement a custom type coverter for DateTime type for JsonSerializer in the format of JSON date ticks.
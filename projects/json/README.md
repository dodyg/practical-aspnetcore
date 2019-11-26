
# Json

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
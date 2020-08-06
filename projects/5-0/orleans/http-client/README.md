# HttpClient and Stateless Worker Grain

This same demonstrates how to use HttpClient in a grain. It uses the same DI mechanism in your normal .NET Core app. 

- The `grain` is marked at `[StatelessWorker]` because in **this case**, it keeps no state and Orleans is free to create multiple activations of this grain. You can read more about **Stateless Worker** grains [here](https://dotnet.github.io/orleans/Documentation/grains/stateless_worker_grains.html).
- We are using the new `System.Json.Text` serializer new in .NET 5 `ReadFromJsonAsync` method.
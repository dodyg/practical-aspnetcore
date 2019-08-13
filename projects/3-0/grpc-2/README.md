# gRPC Server Streaming

This is a sample of gRPC [Server Streaming](https://grpc.io/docs/guides/concepts/). In this case the server just repeat the same message 10 times every 5 seconds. Make sure you run both the server and the client.

Note: The library `Grpc.Net.Client` is still in active development. There is an extension method called [`ReadAllAsync`](https://github.com/grpc/grpc-dotnet/blob/88bae72a688ea066ed56f9006b6b64ae1b1786b6/src/Grpc.Net.Common/AsyncStreamReaderExtensions.cs) that makes reading stream data from the server much easier that's not available yet.
# gRPC Client/Server bidirectional Streaming

This is a sample of gRPC [bidirectional Streaming](https://grpc.io/docs/guides/concepts/). In this case the client is sending a ping with a delay timing that increment by one second for every server's pong.

This interaction will run virtually forever.
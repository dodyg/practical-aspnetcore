# CoreWCF Hello World

This sample consists of a server using CoreWCF to host a simple WCF service accepting simple HTTP SOAP requests.

The client side is located on `client` folder and the server at the `server` folder. Make sure you run the server **first** before running the client.

Both programs share the same service definition (`IEchoService.cs`). The server uses the types defined in `CoreWCF.Primitives`, the client uses the types defined in `System.ServiceModel.Primitives`.
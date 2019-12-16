# Web Sockets

**Warning**: These samples are low level websocket code. For production, use [SignalR](https://github.com/aspnet/signalr). Yes I will work on SignalR samples soon.

* [Echo Server](web-sockets-1)

  This is the simplest web socket code you can write. It simply returns what you sent. It does not handle the closing of the connection. It does not handle data that is larger than buffer. It only handles text payload.

* [Echo Server 2](web-sockets-2)

  We improve upon the previous sample by adding console logging (requiring ```Microsoft.Extensions.Logging.Console``` package) and handling data larger than the buffer. I set the buffer to be very small (4 bytes) so you can see how it works.

* [Echo Server 3](web-sockets-3)

  We improve upon the previous sample by enabling broadcast. What you see here is a very crude chat functionality.

* [Echo Server 4](web-sockets-4)

  We improve upon the previous sample by handling closing event intiated by the web client.

* [Chat Server](web-sockets-5)

  Implement a rudimentary single channel chat server.

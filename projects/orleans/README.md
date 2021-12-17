# Microsoft Orleans (12)

These are simple samples to play with [Microsoft Orleans](https://github.com/dotnet/orleans), a cross-platform framework for building robust, scalable distributed applications.

This section is very early in development. My experience in using an Actor framework is ZERO. Welcome to the world of Grains and Silo.

## Samples

- [Hello World](hello-world)

  This sample is a sample from Orleans that I simplify and port to C# 9. You need to run two executables.

- [Hello World with Redis storage](hello-world-2)

  In the previous Hello World sample, once you stop the `silo`, the messages are gone. In this sample we use Redis to store the Grain between `silo` restarts so we won't lose the messages.

- [ASP.NET Core and Orleans](hello-world-3)

  Co-host Orleans and ASP.NET Core together. Everything in a single `Program.cs` file. This sample is the simplest.

- [ASP.NET Core and Orleans with Redis storage](hello-world-4)

  Co-host Orleans and ASP.NET Core together with Redis storage. Everything in a single `Program.cs` file. This sample also use C# 9 records.

- [Timer](timer)

  This sample demonstrates the functionality of `Grain.RegisterTimer`. It's useful to trigger actions to be repeated frequently (less than every minute). 

- [Reminder](reminder)

  This sample demonstrates the functionality of `Grain.RegisterOrUpdateReminder`. It's useful to trigger actions to be repeated infrequently (more than every minute, hours or days). This is a persistent timer that survives grain restarts. [Reminder is much expensive than Timer](https://github.com/dotnet/orleans/issues/4218#issuecomment-373162275).

- [HttpClient and Stateless Worker Grain](http-client)

  This sample demonstrates using HttpClient in a `grain` and also introduces the concept of a Stateless Worker `grain`. 

- [RSS Reader - Simple](rss-reader)

  This is a simple RSS Reader that reads syndication items and only store and display unique ones. 

- [RSS Reader with Reminder](rss-reader-2)

  This is an RSS Reader that reads syndication items and refresh each feed source every x minutes (configurable). 

- [RSS Reader with Reminder + Subscription List](rss-reader-3)

  This is an RSS reader that read RSS feed sources from an OPML subscription list and refresh each feed source every x minutes (configurable). It uses a single Reminder grain to handle all the reminders created for each feed.

- [RSS Reader with Reminder + Subscription List 2](rss-reader-4)

  This is an RSS reader that read RSS feed sources from an OPML subscription list and refresh each feed source every x minutes (configurable). It uses one Reminder grain for each feed.

- [RSS Reader with Reminder + Subscription List + Orleans Streams](rss-reader-5)

  This is an RSS reader that read RSS feed sources from an OPML subscription list and publish it into a single stream with a single channel. The data then processed by implicit susbcriber. 

dotnet6
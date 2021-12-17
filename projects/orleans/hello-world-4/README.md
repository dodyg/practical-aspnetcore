# Orleans Hello World with ASP.NET Core and Redis Storage

**This sample requires redis**. Make sure to run FLUSHALL in redis-cli between samples.

This sample shows how to use Orleans with ASP.NET Core. In this case we are using Redis Storage. This means that all the messages persist beyond restart.

- Make sure you have redis installed and running.
- Run the app using `dotnet run`.
- Open `localhost:5000`
- Keep refeshing the browser. You will see the messages will continue to grow.
- Close the app. Repeat the steps. You will see that the messages persists between restarts.

We are using C# records in this sample. It works fine.
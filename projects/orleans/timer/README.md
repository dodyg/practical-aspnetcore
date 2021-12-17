# Timer

**This sample requires redis**. Make sure to run FLUSHALL in redis-cli between samples.

This is a sample for `Grain.RegisterTimer` method. You can find out more about this functionality [here](https://dotnet.github.io/orleans/1.5/Documentation/Core-Features/Timers-and-Reminders.html).

- Make sure you have redis installed and running.
- Run the app using `dotnet run`.
- Open `localhost:5000`
- When you open the page the first time, there's a timer that will add a new message every 5 seconds.

We are using C# records in this sample. It works fine.
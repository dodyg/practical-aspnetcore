# Reminder

**This sample requires redis**. Make sure to run FLUSHALL in redis-cli between samples.

This is a sample for `Grain.RegisterOrUpdateReminder` method. You can find out more about this functionality [here](https://dotnet.github.io/orleans/1.5/Documentation/Core-Features/Timers-and-Reminders.html).

- Make sure you have redis installed and running.
- Run the app using `dotnet run`.
- Open `localhost:5000`
- When you open the page the first time, click on "Set reminder" link. It will start a reminder that will repeat every 1 minute (which is the most minimum value allowed for reminder).
- This reminder will keep running until you stop it by clicking on "Remove reminder" or you restart the `silo`.

We are using C# records in this sample. It works fine.
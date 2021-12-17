# RSS Reader with Reminder + Subscription list

**This sample requires redis**. Make sure to run FLUSHALL in redis-cli between samples.

This is a simple RSS reader that uses two storage, one for storing a feed source and another for storing feed results. You can keep refreshing your browser and the RSS Reader will display the latest results whenever they are available. It also logs the result of every RSS feed that got fetch regularly using Orleans reminder.

- Make sure you have redis installed and running.
- Run the app using `dotnet run`.
- Open `localhost:5000`
- You can keep refreshing the browser page as much as you want and it will only pick up and store unique feed items.
- Orleans will keep keep refreshing each feed every x minutes (configurable). 
- This Rss Reader will read a list of RSS sources from an OPML subscription feed http://scripting.com/misc/mlb.opml

In this RSS feed we use a single Reminder grain to handle all the reminders created for each feed. An alternative approach would be to use one Reminder grain per Reminder (this is demonstrated in another sample [RSS Reader with Reminder + Subscription list 2](../rss-reader-4)). 
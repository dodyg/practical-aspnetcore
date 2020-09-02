# RSS Reader with Reminder with subscription list

This is a simple RSS reader that uses two storage, one for storing a feed source and another for storing feed results. You can keep refreshing your browser and the RSS Reader will display the latest results whenever they are available.

- Make sure you have redis installed and running.
- Run the app using `dotnet run`.
- Open `localhost:5000`
- You can keep refreshing the browser page as much as you want and it will only pick up and store unique feed items.
- Orleans will keep keep refreshing each feed every x minutes (configurable). 
- This Rss Reader will read a list of RSS sources from an OPML subscription feed http://scripting.com/misc/mlb.opml

In this RSS feed we use a single reminder grain to handle all the reminders created for each feed. An alternative approach would be to use one reminder grain per reminder (this is in another sample). 
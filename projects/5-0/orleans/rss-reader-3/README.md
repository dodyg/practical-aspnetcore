# RSS Reader with Reminder

This is a simple RSS reader that uses two storage, one for storing a feed source and another for storing feed results. You can keep refreshing your browser and the RSS Reader will display the latest results whenever they are available.

- Make sure you have redis installed and running.
- Run the app using `dotnet run`.
- Open `localhost:5000`
- You can keep refreshing the browser page as much as you want and it will only pick up and store unique feed items.
- Orleans will keep keep refreshing each feed every x minutes (configurable). 


Subscription List Sources

- http://scripting.com/misc/mlb.opml
http://scripting.com/river/
		"Dave": "http://radio3.io/rivers/iowa.js",
		"NYT": "http://radio3.io/rivers/nytRiver.js",
		"Tech": "http://radio3.io/rivers/tech.js",
		"World": "http://radio3.io/rivers/world.js",
		"Bloggers": "http://radio3.io/rivers/bloggers.js",
		"Podcasts": "http://rssforpodcatch.scripting.com/rivers/podcasts.js",
		"NBA": "http://radio3.io/rivers/nba.js",
		"Movies": "http://radio3.io/rivers/movies.js",
		"Pol": "http://radio3.io/rivers/politics.js"
        https://github.com/scripting/river5/tree/master/lists
        https://hnrss.org/newest

        https://docs.github.com/en/rest/reference/activity#feeds
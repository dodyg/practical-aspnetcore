# Rss Reader 

This version of RSS Reader uses C# 8.0 `IAsyncEnumerable` to process RSS data as they are available. There is an artificial `await Task.Delay(5000);` added to `RssNews.GetNewsAsync` so you can see visually how the UI changes.

The feed is downloaded with an explicit `User-Agent` header via `HttpClient` before parsing, because some feeds (e.g. `hnrss.org`) reject requests without one.
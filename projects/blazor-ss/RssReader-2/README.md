# Rss Reader 

This version of RSS Reader uses C# 8.0 `IAsyncEnumerable` to process RSS data as they are available. There is an artificial `await Task.Delay(3000);` added to `RssNews.GetNewsAsync` so you can see visually how the UI changes.
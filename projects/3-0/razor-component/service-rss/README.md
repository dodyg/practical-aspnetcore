# Rss Reader

This samples shows that Razor Component is truly a server side system. You will see how we fetch RSS data on the server and deliver it to your browser.

This sample also shows the use of `MarkupString` to show unescape HTML string.

```
<ul>
    @foreach(var n in News)
    {
        <li>@((MarkupString)n.Description)</li>
    }
</ul>
```

Run the sample on `RssReader.Server` using `dotnet watch`.
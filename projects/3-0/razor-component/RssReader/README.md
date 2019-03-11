# Rss Reader

This sample shows that Razor Component is truly a server side system. You will see how we fetch RSS data on the server and deliver it to your browser. It uses `Microsoft.SyndicationFeed.ReaderWriter` package to parse an external RSS XML document. 

This sample also shows the use of `MarkupString` to show unescaped HTML string.

```
<ul>
    @foreach(var n in News)
    {
        <li>@((MarkupString)n.Description)</li>
    }
</ul>
```

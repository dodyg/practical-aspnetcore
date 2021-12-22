using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System.Xml;
using System.Text;

var app = WebApplication.Create();

app.Run(async context =>
{
    var parser = new RssParser();
    var items = new List<OutlineSyndicationItem>();

    using (var xmlReader = XmlReader.Create("http://scripting.com/rss.xml", new XmlReaderSettings { Async = true }))
    {
        var feedReader = new RssFeedReader(xmlReader);

        while (await feedReader.Read())
        {
            switch (feedReader.ElementType)
            {
                case SyndicationElementType.Item:
                    //ISyndicationContent is a raw representation of the feed
                    ISyndicationContent content = await feedReader.ReadContent();

                    ISyndicationItem item = parser.CreateItem(content);
                    ISyndicationContent outline = content.Fields.FirstOrDefault(f => f.Name == "source:outline");

                    items.Add(new OutlineSyndicationItem(item, outline));
                    break;
                default:
                    break;
            }
        }
    }

    var str = new StringBuilder();
    str.Append("<ul>");
    foreach (var i in items)
    {
        str.Append($"<li>{i.Item.Description} - <span style=\"color:red;\">");
        if (i.Outline != null)
        {
            str.Append("<ul>");
            foreach (var o in i.Outline.Attributes)
            {
                str.Append($"<li>{o.Key} - {o.Value}</li>");
            }
            str.Append("</ul>");
        }
        str.Append("</li>");
    }
    str.Append("</ul>");

    context.Response.Headers.Add("Content-Type", "text/html");
    await context.Response.WriteAsync($@"
    <html>
        <head>
            <link rel=""stylesheet"" type=""text/css"" href=""http://fonts.googleapis.com/css?family=Germania+One"">
            <style>
                body {{
                font-family: 'Germania One', serif;
                font-size: 24px;
            }}
            </style>
        </head>
        <body>
            {str.ToString()}
        </body>
    </html>
    ");
});

app.Run();

public class Outline
{
    public string Text { get; set; }

    public string this[string key] => Attributes.ContainsKey(key) ? Attributes[key] : null;

    public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
}

public class OutlineSyndicationItem
{
    public ISyndicationItem Item { get; }

    public Outline Outline { get; }

    public OutlineSyndicationItem(ISyndicationItem basic, ISyndicationContent outline)
    {
        Item = basic;
        if (outline != null)
        {
            Outline = new Outline
            {
                Text = outline.Attributes.FirstOrDefault(x => x.Name.Equals("text", StringComparison.OrdinalIgnoreCase))?.Value,
                Attributes = outline.Attributes.ToDictionary(x => x.Name, x => x.Value)
            };
        }
    }
}

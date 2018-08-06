using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System.Net.Http;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;

namespace StartupBasic
{
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

    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //These are the four default services available at Configure
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
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}
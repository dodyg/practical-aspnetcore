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

namespace StartupBasic
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //These are the four default services available at Configure
            app.Run(async context =>
            {
                var items = new List<SyndicationItem>();

                using (var xmlReader = XmlReader.Create("http://scripting.com/rss.xml", new XmlReaderSettings { Async = true }))
                {
                    var feedReader = new RssFeedReader(xmlReader);

                    while (await feedReader.Read())
                    {
                        switch (feedReader.ElementType)
                        {
                            case SyndicationElementType.Item:
                                ISyndicationItem item = await feedReader.ReadItem();
                                items.Add(new SyndicationItem(item));
                                break;
                            default:
                                break;
                        }
                    }
                }

                var str = new StringBuilder();
                str.Append("<ul>");
                foreach(var i in items)
                {
                    str.Append($"<li>{i.Description}</li>");
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
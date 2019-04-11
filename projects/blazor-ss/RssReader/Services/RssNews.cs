using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System.Xml;
using System.Text;
namespace RssReader.Services
{
    public class RssNews
    {
        public async Task<List<SyndicationItem>> GetNewsAsync()
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

            return items;
        }
    }
}
using System.Xml;
using RssReaderWebApplication.Models;

namespace RssReaderWebApplication;

public static class RssFeedService
{
    public static List<RssItem> GetRssFeedItems(string rssUrl)
    {
        var rssItems = new List<RssItem>();
        var rssXmlDoc = new XmlDocument();
        
        rssXmlDoc.Load(rssUrl);
        var rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

        if (rssNodes == null) 
            return rssItems;

        rssItems.AddRange(
            from XmlNode rssNode in rssNodes 
            select new RssItem
            {
                Title = rssNode.SelectSingleNode("title").InnerText, 
                Link = rssNode.SelectSingleNode("link").InnerText, 
                Description = rssNode.SelectSingleNode("description").InnerText
            });

        return rssItems;
    }
}
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System.Xml;
namespace RssReader.Services
{
    public class RssNews
    {
        private static readonly HttpClient httpClient = CreateHttpClient();

        // hnrss.org and some other feeds reject requests without a User-Agent header,
        // so fetch the feed with an explicit UA instead of letting XmlReader do it
        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; practical-aspnetcore-rss-sample/1.0)");
            return client;
        }

        public async IAsyncEnumerable<List<SyndicationItem>> GetMultipleNewsAsync(params string[] news)
        {
            foreach (var x in news)
            {
                yield return await GetNewsAsync(x);
            }
        }

        public async Task<List<SyndicationItem>> GetNewsAsync(string url)
        {
            var items = new List<SyndicationItem>();

            using (var stream = await httpClient.GetStreamAsync(url))
            using (var xmlReader = XmlReader.Create(stream, new XmlReaderSettings { Async = true }))
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

            await Task.Delay(5000);

            return items;
        }
    }
}
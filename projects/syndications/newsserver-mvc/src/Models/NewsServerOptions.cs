namespace NewsServer.Models
{
    public class NewsServerOptions
    {
        public const string NewsServer = "NewsServer";

        public FeedOption[] Feeds { get; set; }
    }

    public class FeedOption
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}

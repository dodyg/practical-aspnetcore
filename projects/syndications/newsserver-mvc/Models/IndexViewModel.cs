using System.ServiceModel.Syndication;

namespace NewsServer.Models
{
    public class IndexViewModel
    {
        public FeedOption[] Feeds { get; set; }
        public FeedOption SelectedFeedOption { get; set; }
        public SyndicationFeed CurrentFeed { get; set; }
    }
}

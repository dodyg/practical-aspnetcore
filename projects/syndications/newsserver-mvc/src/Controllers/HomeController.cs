using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsServer.Models;

namespace NewsServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsServerOptions _newsServerOptions;

        public HomeController(NewsServerOptions newsServerOptions)
        {
            _newsServerOptions = newsServerOptions;
        }

        public IActionResult Index(string slug)
        {
            var feedList = _newsServerOptions.Feeds;
            var selectedFeedOption = GetCurrentFeed(slug, feedList);
            var currentFeed = GetFeedItems(selectedFeedOption);
            var model = new IndexViewModel
            {
                Feeds = _newsServerOptions.Feeds,
                SelectedFeedOption = selectedFeedOption,
                CurrentFeed = currentFeed
            };
            return View(model);
        }

        private SyndicationFeed GetFeedItems(FeedOption currentFeed)
        {
            using var reader = XmlReader.Create(currentFeed.Url, new XmlReaderSettings { Async = true });
            return SyndicationFeed.Load(reader);
        }

        private FeedOption GetCurrentFeed(string slug, FeedOption[] feedList)
        {
            FeedOption currentFeed;
            if (string.IsNullOrEmpty(slug))
            {
                currentFeed = feedList[0];
            }
            else
            {
                currentFeed = feedList.Where(item => item.Name == slug).FirstOrDefault();
            }
            return currentFeed;
        }
    }
}

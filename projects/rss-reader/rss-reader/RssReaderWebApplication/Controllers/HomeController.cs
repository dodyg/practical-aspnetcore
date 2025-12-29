using Microsoft.AspNetCore.Mvc;

namespace RssReaderWebApplication.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        const string rssUrl = "https://www.newyorker.com/feed/everything";
        var rssItems = RssFeedService.GetRssFeedItems(rssUrl);
        return View(rssItems);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SyndicationFeed;

record FeedChannel
{
    public string? Title { get; set; }

    public string? Website { get; set; }

    public Uri? Url { get; set; }

    public bool HideTitle { get; set; }

    public bool HideDescription { get; set; }
}

class FeedSource
{
    public string Url { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? Website { get; set; }

    public bool HideTitle { get; set; }

    public bool HideDescription { get; set; }

    public short UpdateFrequencyInMinutes { get; set; } = 1;

    public bool CanFetch ()=> History.Take(10).Count(x => !x.IsValid) <= 3;  

    public bool IsLatestValid 
    {
        get
        {
            if (History.Count == 0)
                return true;

            return History.First().IsValid;
        }
    }

    public List<FeedHistory> History { get; set; } = new List<FeedHistory>();

    public void LogFetchAttempt(bool isValid, string? message = null) =>
        History.Insert(0, new FeedHistory { Timestamp = DateTimeOffset.UtcNow, IsValid = isValid, Message = message });

    public FeedChannel ToChannel()
    {
        return new FeedChannel
        {
            Title = Title,
            Website = Website,
            HideTitle = HideTitle,
            HideDescription = HideDescription
        };
    }
}

record FeedHistory 
{
    public DateTimeOffset Timestamp { get; set; }

    public bool IsValid { get; set; }

    public string? Message { get; set; }
}

record FeedItem
{
    public FeedChannel? Channel { get; set; }

    public string? Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public Uri? Url { get; set;}

    public DateTimeOffset PublishedOn { get; set; }

    public FeedItem()
    {

    }

    public FeedItem(FeedChannel channel, SyndicationItem item)
    {
        Channel = channel;
        Id = item.Id;
        Title = item.Title;
        Description = item.Description;
        var link = item.Links.FirstOrDefault();
        if (link is object)
            Url = link.Uri;        

        if (item.LastUpdated == default(DateTimeOffset))
            PublishedOn = item.Published;
        else
            PublishedOn = item.LastUpdated;
    }
}

enum FeedType
{
    Atom,
    Rss
}

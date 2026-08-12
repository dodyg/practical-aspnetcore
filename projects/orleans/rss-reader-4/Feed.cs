using Microsoft.SyndicationFeed;
using Orleans;

[GenerateSerializer]
record FeedChannel
{
    [Id(0)]
    public string? Title { get; set; }

    [Id(1)]
    public string? Website { get; set; }

    [Id(2)]
    public Uri? Url { get; set; }

    [Id(3)]
    public bool HideTitle { get; set; }

    [Id(4)]
    public bool HideDescription { get; set; }
}

[GenerateSerializer]
class FeedSource
{
    [Id(0)]
    public string Url { get; set; } = string.Empty;

    [Id(1)]
    public string Title { get; set; } = string.Empty;

    [Id(2)]
    public string? Website { get; set; }

    [Id(3)]
    public bool HideTitle { get; set; }

    [Id(4)]
    public bool HideDescription { get; set; }

    [Id(5)]
    public short UpdateFrequencyInMinutes { get; set; } = 1;

    public bool CanFetch() => History.Take(10).Count(x => !x.IsValid) <= 3;

    public bool IsLatestValid
    {
        get
        {
            if (History.Count == 0)
                return true;

            return History.First().IsValid;
        }
    }

    [Id(6)]
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

[GenerateSerializer]
record FeedHistory
{
    [Id(0)]
    public DateTimeOffset Timestamp { get; set; }

    [Id(1)]
    public bool IsValid { get; set; }

    [Id(2)]
    public string? Message { get; set; }
}

[GenerateSerializer]
record FeedItem
{
    [Id(0)]
    public FeedChannel? Channel { get; set; }

    [Id(1)]
    public string? Id { get; set; }

    [Id(2)]
    public string? Title { get; set; }

    [Id(3)]
    public string? Description { get; set; }

    [Id(4)]
    public Uri? Url { get; set; }

    [Id(5)]
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

[GenerateSerializer]
enum FeedType
{
    Atom,
    Rss
}

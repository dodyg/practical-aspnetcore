using System.Net;
using Orleans;
using Orleans.Runtime;
using Orleans.Configuration;
using Orleans.Hosting;
using System.Xml;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using Orleans.Streams;

var builder = WebApplication.CreateBuilder();
builder.Services.AddHttpClient();
builder.Logging.SetMinimumLevel(LogLevel.Information).AddConsole();
builder.Host.UseOrleans(builder =>
{
    builder
        .UseLocalhostClustering()
        .UseInMemoryReminderService()
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "dev";
            options.ServiceId = "http-client";
        })
        .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(FeedSourceGrain).Assembly).WithReferences())
        .AddRedisGrainStorage(Config.RedisStorage, optionsBuilder => optionsBuilder.Configure(options =>
        {
            options.ConnectionString = "localhost:6379";
            options.UseJson = true;
            options.DatabaseNumber = 1;
        }))
        .AddMemoryGrainStorage("PubSubStore")
        .AddSimpleMessageStreamProvider(Config.StreamProvider);
});

var app = builder.Build();

app.MapGet("/", async context =>
{
    var httpClientFactory = context.RequestServices.GetService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient();

    var opmlSubscriptionList = await httpClient.GetStringAsync("http://scripting.com/misc/mlb.opml");
    var opml = new Opml(opmlSubscriptionList);
    var subscriptionList = new RssSubscription(opml);

    var client = context.RequestServices.GetService<IGrainFactory>()!;
    var feedSourceGrain = client.GetGrain<IFeedSource>(0)!;

    var logger = context.RequestServices.GetService<ILoggerFactory>()!.CreateLogger("rss-reader");

    foreach (var source in subscriptionList.Items)
    {
        logger.LogInformation("Adding " + source.XmlUri?.ToString() ?? String.Empty);
        await feedSourceGrain.AddAsync(new FeedSource
        {
            Url = source.XmlUri?.ToString() ?? string.Empty,
            Website = source.HtmlUri?.ToString() ?? string.Empty,
            Title = source.Title ?? string.Empty,
            UpdateFrequencyInMinutes = 3
        });
    }

    var sources = await feedSourceGrain.GetAllAsync();

    foreach (var s in sources)
    {
        var feedFetcherReminderGrain = client.GetGrain<IFeedFetcherReminder>(s.Url);
                    // AddReminder is indempotent
                    await feedFetcherReminderGrain.AddReminder(s.Url, s.UpdateFrequencyInMinutes);
    }

    var feedResultsGrain = client.GetGrain<IFeedItemResults>(0);
    var feedItems = await feedResultsGrain.GetAllAsync();

    await context.Response.WriteAsync(@"<html>
                    <head>
                        <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/uikit@3.5.5/dist/css/uikit.min.css"" />
                        <title>Orleans RSS Reader</title>
                    </head>");
    await context.Response.WriteAsync("<body><div class=\"uk-container\">");
    await context.Response.WriteAsync("<a href=\"/feed-sources\">Feed Sources</a><br/>");
    if (feedItems.Count == 0)
        await context.Response.WriteAsync("<p>Please refresh your browser again if you see no feeds displayed.</p>");

    await context.Response.WriteAsync("<ul class=\"uk-list\">");
    foreach (var i in feedItems)
    {
        await context.Response.WriteAsync("<li class=\"uk-card uk-card-default uk-card-body\">");
        if (!string.IsNullOrWhiteSpace(i.Title))
            await context.Response.WriteAsync($"{ i.Title }<br/>");

        await context.Response.WriteAsync(i.Description ?? "");

        if (i.Url is object)
            await context.Response.WriteAsync($"<br/><a href=\"{i.Url}\">link</a>");

        await context.Response.WriteAsync($"<div style=\"font-size:small;\">published on: {i.PublishedOn}</div>");
        await context.Response.WriteAsync($"<div style=\"font-size:small;\">source: <a href=\"{i.Channel?.Website}\">{i.Channel?.Title}</a></div>");
        await context.Response.WriteAsync("</li>");
    }
    await context.Response.WriteAsync("</ul>");
    await context.Response.WriteAsync("</div></body></html>");
});

app.MapGet("/feed-sources", async context =>
{
    var client = context.RequestServices.GetService<IGrainFactory>()!;
    var feedSourceGrain = client.GetGrain<IFeedSource>(0)!;
    var sources = await feedSourceGrain.GetAllAsync();

    await context.Response.WriteAsync(@"<html>
                    <head>
                        <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/uikit@3.5.5/dist/css/uikit.min.css"" />
                        <title>Orleans RSS Reader</title>
                    </head>");
    await context.Response.WriteAsync("<body><div class=\"uk-container\">");
    await context.Response.WriteAsync("<a href=\"/\">Home</a><br/>");
    await context.Response.WriteAsync("<strong>Valid</strong>");
    await context.Response.WriteAsync("<ul>");
    foreach (var s in sources.Where(x => x.IsLatestValid))
    {
        await context.Response.WriteAsync($"<li><a href=\"{s.Url}\">{s.Url}</a>");
        await context.Response.WriteAsync("<ul>");
        foreach (var h in s.History)
        {
            await context.Response.WriteAsync($"<li>{h.Timestamp} - {h.IsValid}<p>{h.Message}</p></li>");
        }
        await context.Response.WriteAsync("</ul>");
        await context.Response.WriteAsync("</li>");
    }
    await context.Response.WriteAsync("</ul>");

    await context.Response.WriteAsync("<strong>Invalid</strong>");
    await context.Response.WriteAsync("<ul>");
    foreach (var s in sources.Where(x => !x.IsLatestValid))
    {
        await context.Response.WriteAsync($"<li><a href=\"{s.Url}\">{s.Url}</a>");
        await context.Response.WriteAsync("<ul>");
        foreach (var h in s.History)
        {
            await context.Response.WriteAsync($"<li>{h.Timestamp} - {h.IsValid}<p>{h.Message}</p></li>");
        }
        await context.Response.WriteAsync("</ul>");
        await context.Response.WriteAsync("</li>");
    }
    await context.Response.WriteAsync("</ul>");

    await context.Response.WriteAsync("</div></body></html>");
});

app.Run();

static class Config
{
    public const string RedisStorage = "redis-rss-reader-5";

    public const string StreamProvider = "SMSProvider";

    public static readonly Guid StreamId = Guid.NewGuid();

    public const string StreamChannel = "RSS";
}

class FeedFetcherReminder : Grain, IRemindable, IFeedFetcherReminder
{
    readonly IGrainFactory _grainFactory;
    readonly ILogger _logger;

    public FeedFetcherReminder(IGrainFactory grainFactory, ILogger<FeedFetcherReminder> logger)
    {
        _grainFactory = grainFactory;
        _logger = logger;
    }

    public async Task AddReminder(string reminder, short repeatEveryMinute)
    {
        if (string.IsNullOrWhiteSpace(reminder))
            throw new ArgumentNullException(nameof(reminder));

        var r = await GetReminder(reminder);

        if (r is not object)
            await RegisterOrUpdateReminder(reminder, dueTime: TimeSpan.FromSeconds(1), period: TimeSpan.FromMinutes(repeatEveryMinute));
    }

    public async Task ReceiveReminder(string reminderName, TickStatus status)
    {
        _logger.Info($"Receive {reminderName} reminder");

        var feedSourceGrain = _grainFactory.GetGrain<IFeedSource>(0)!;

        var feedSource = await feedSourceGrain.FindFeedSourceByUrlAsync(reminderName);

        if (feedSource is object)
        {
            _logger.Info($"Fetching {feedSource.Url}");
            var feedFetcherGrain = _grainFactory.GetGrain<IFeedFetcher>(feedSource.Url);
            await feedFetcherGrain.FetchAsync(feedSource);
        }
    }
}

[ImplicitStreamSubscription(Config.StreamChannel)]
class FeedStreamReaderGrain : Grain, IFeedStreamReader
{
    readonly ILogger _logger;

    readonly IGrainFactory _grainFactory;

    public FeedStreamReaderGrain(ILogger<FeedStreamReaderGrain> logger, IGrainFactory grainFactory)
    {
        _logger = logger;
        _grainFactory = grainFactory;
    }

    public override async Task OnActivateAsync()
    {
        var streamProvider = GetStreamProvider(Config.StreamProvider);
        var stream = streamProvider.GetStream<List<FeedItem>>(Config.StreamId, Config.StreamChannel);

        var feedItemResultGrain = _grainFactory.GetGrain<IFeedItemResults>(0);
        await stream.SubscribeAsync<List<FeedItem>>(async (data, token) =>
        {
            _logger.Info($"Feed Items {data.Count}");
            await feedItemResultGrain.AddAsync(data);
        });
    }
}

class FeedItemResultGrain : Grain, IFeedItemResults
{
    private readonly IPersistentState<FeedItemStore> _storage;

    public FeedItemResultGrain([PersistentState("feed-item-results-5", Config.RedisStorage)] IPersistentState<FeedItemStore> storage) => _storage = storage;

    public async Task AddAsync(List<FeedItem> items)
    {
        //make sure there is no duplication
        foreach (var i in items.Where(x => !string.IsNullOrWhiteSpace(x.Id)))
        {
            if (!_storage.State.Results.Exists(x => x.Id?.Equals(i.Id, StringComparison.OrdinalIgnoreCase) ?? false))
                _storage.State.Results.Add(i);
        }
        await _storage.WriteStateAsync();
    }

    public Task<List<FeedItem>> GetAllAsync() => Task.FromResult(_storage.State.Results.OrderByDescending(x => x.PublishedOn).ToList());

    public async Task ClearAsync()
    {
        _storage.State.Results.Clear();
        await _storage.WriteStateAsync();
    }
}

record FeedItemStore
{
    public List<FeedItem> Results { get; set; } = new List<FeedItem>();
}

class FeedSourceGrain : Grain, IFeedSource
{
    private readonly IPersistentState<FeedSourceStore> _storage;

    public FeedSourceGrain([PersistentState("feed-source-5", Config.RedisStorage)] IPersistentState<FeedSourceStore> storage) => _storage = storage;

    public async Task AddAsync(FeedSource source)
    {
        if (string.IsNullOrWhiteSpace(source.Url))
            return;

        if (_storage.State.Sources.Find(x => x.Url == source.Url) is null)
        {
            _storage.State.Sources.Add(source);
            await _storage.WriteStateAsync();
        }
    }

    public Task<List<FeedSource>> GetAllAsync() => Task.FromResult(_storage.State.Sources);

    public Task<FeedSource?> FindFeedSourceByUrlAsync(string url) =>
        Task.FromResult(_storage.State.Sources.Find(x => x.Url.Equals(url, StringComparison.Ordinal)));

    public async Task<FeedSource?> UpdateFeedSourceStatus(string url, bool activeStatus, string? message)
    {
        var feed = await FindFeedSourceByUrlAsync(url);
        if (feed is object)
        {
            feed.LogFetchAttempt(activeStatus, message);
            await _storage.WriteStateAsync();
        }

        return feed;
    }
}

record FeedSourceStore
{
    public List<FeedSource> Sources { get; set; } = new List<FeedSource>();
}

class FeedFetchGrain : Grain, IFeedFetcher
{
    readonly IGrainFactory _grainFactory;

    readonly ILogger _logger;

    readonly IHttpClientFactory _httpClientFactory;

    public FeedFetchGrain(IGrainFactory grainFactory, ILogger<FeedFetchGrain> logger, IHttpClientFactory httpClientFactory)
    {
        _grainFactory = grainFactory;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task FetchAsync(FeedSource source)
    {
        var results = await ReadFeedAsync(source);

        var streamProvider = GetStreamProvider(Config.StreamProvider);
        var stream = streamProvider.GetStream<List<FeedItem>>(Config.StreamId, Config.StreamChannel);

        await stream.OnNextAsync(results);
    }

    public async Task<List<FeedItem>> ReadFeedAsync(FeedSource source)
    {
        if (string.IsNullOrWhiteSpace(source.Url))
            return new List<FeedItem>();

        if (!source.CanFetch())
            return new List<FeedItem>();

        var feed = new List<FeedItem>();
        FeedType feedType = FeedType.Rss;
        try
        {
            _logger.LogInformation($"Fetching {source.Url}");

            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(10);
            var response = await client.GetAsync(source.Url.ToString());

            var memory = new MemoryStream();
            await response.Content.CopyToAsync(memory);

            memory.Seek(0, SeekOrigin.Begin);
            char[] buf = new char[400]; // We need large buffer because to skip xml metadata and comments before the root of the xml document starts
            var sr = new StreamReader(memory);
            var charRead = sr.ReadBlock(buf, 0, buf.Length);

            if (!new string(buf).Contains("rss", StringComparison.OrdinalIgnoreCase))
                feedType = FeedType.Atom;

            memory.Seek(0, SeekOrigin.Begin);
            using var xmlReader = XmlReader.Create(memory, new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore });

            if (feedType == FeedType.Rss)
            {
                var feedReader = new RssFeedReader(xmlReader);
                // Read the feed
                while (await feedReader.Read())
                {
                    switch (feedReader.ElementType)
                    {
                        // Read Item
                        case SyndicationElementType.Item:
                            var item = await feedReader.ReadItem();
                            feed.Add(new FeedItem(source.ToChannel(), new SyndicationItem(item)));
                            break;

                        default:
                            var content = await feedReader.ReadContent();

                            break;
                    }
                }
            }
            else
            {
                var feedReader = new AtomFeedReader(xmlReader);

                while (await feedReader.Read())
                {
                    switch (feedReader.ElementType)
                    {
                        // Read Item
                        case SyndicationElementType.Item:
                            var entry = await feedReader.ReadEntry();
                            feed.Add(new FeedItem(source.ToChannel(), new SyndicationItem(entry)));
                            break;

                        default:
                            var content = await feedReader.ReadContent();
                            break;
                    }
                }
            }

            var feedSource = _grainFactory.GetGrain<IFeedSource>(0)!;
            await feedSource.UpdateFeedSourceStatus(source.Url, true, $"{feed.Count} items fetched");

            return feed;
        }
        catch (Exception ex)
        {
            _logger.LogError($"({feedType}) {source.Url} Exception: {ex.Message}");

            // Mark feed as invalid
            var feedSource = _grainFactory.GetGrain<IFeedSource>(0)!;
            await feedSource.UpdateFeedSourceStatus(source.Url, false, ex.Message);

            return new List<FeedItem>();
        }
    }
}

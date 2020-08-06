using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Runtime;
using Orleans.Configuration;
using Orleans.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Xml;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;

await Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
    .ConfigureLogging(builder =>
    {
        builder.SetMinimumLevel(LogLevel.Information);
        builder.AddConsole();
    })
    .UseOrleans(builder =>
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
            .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloReminderGrain).Assembly).WithReferences())
            .AddRedisGrainStorage("redis-http-client", optionsBuilder => optionsBuilder.Configure(options =>
            {
                options.DataConnectionString = "localhost:6379";
                options.UseJson = true;
                options.DatabaseNumber = 1;
            }));
    })
    .RunConsoleAsync();

class Startup
{
    IHostEnvironment _env;

    public Startup(IHostEnvironment env)
    {
        _env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (_env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                IGrainFactory client = context.RequestServices.GetService<IGrainFactory>()!;
                IHelloArchive grain = client.GetGrain<IHelloArchive>(0)!;
                await grain.SayHello("Hello world " + new Random().Next());

                await context.Response.WriteAsync(@"<html><head><link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/uikit@3.5.5/dist/css/uikit.min.css"" /></head>");
                await context.Response.WriteAsync("<body>");
                await context.Response.WriteAsync("Click on Set reminder to start the reminder (it will run every 1 minute). Then refresh this page to see the messages being addded.<br>");
                await context.Response.WriteAsync(@"<a href=""set-reminder"">Set reminder</a> - <a href=""remove-reminder"">Remove reminder</a><br/>");
                await context.Response.WriteAsync("<ul>");
                await context.Response.WriteAsync("</ul>");
                await context.Response.WriteAsync("</body></html>");
            });

            // WARNING - changing state using GET is a terrible terrible practice. I use it here because this is a sample and I am lazy. Don't follow my bad example.
            endpoints.MapGet("/set-reminder", async context =>
            {
                IGrainFactory client = context.RequestServices.GetService<IGrainFactory>()!;
                IHelloArchive grain = client.GetGrain<IHelloArchive>(0)!;

                await grain.AddReminder("repeat-hello", repeatEvery: TimeSpan.FromMinutes(1));

                context.Response.Redirect("/");
            });

            // WARNING - changing state using GET is a terrible terrible practice. I use it here because this is a sample and I am lazy. Don't follow my bad example.
            endpoints.MapGet("/remove-reminder", async context =>
            {
                IGrainFactory client = context.RequestServices.GetService<IGrainFactory>()!;
                IHelloArchive grain = client.GetGrain<IHelloArchive>(0)!;

                await grain.RemoveReminder("repeat-hello");
                context.Response.Redirect("/");
            });
        });
    }
}

public class HelloReminderGrain : Grain, IHelloArchive, IRemindable
{
    private readonly IPersistentState<FeedItem> _archive;
    private readonly ILogger _log;

    private string _greeting = "hello world";

    public HelloReminderGrain([PersistentState("archive", "redis-http-client")] IPersistentState<FeedItem> archive, ILogger<HelloReminderGrain> log)
    {
        _archive = archive;
        _log = log;
    }

    public Task SayHello(string greeting)
    {
        _greeting = greeting;
        return Task.CompletedTask;
    }

    public async Task ReceiveReminder(string reminderName, TickStatus status)
    {
        _log.Info($"Receive reminder {reminderName} on { DateTime.UtcNow } with status { status }");
        //var g = new Greeting(_greeting, DateTime.UtcNow);
        //_archive!.State.Greetings.Insert(0, g);
        await _archive!.WriteStateAsync();
    }

    public async Task AddReminder(string reminder, TimeSpan repeatEvery)
    {
        if (string.IsNullOrWhiteSpace(reminder))
            throw new ArgumentNullException(nameof(reminder));

        var r = await GetReminder(reminder);

        if (r is not object)
            await RegisterOrUpdateReminder(reminder, TimeSpan.FromSeconds(1), repeatEvery);
    }

    public async Task RemoveReminder(string reminder)
    {
        if (string.IsNullOrWhiteSpace(reminder))
            throw new ArgumentNullException(nameof(reminder));

        var r = await GetReminder(reminder);

        if (r is object)
            await UnregisterReminder(r);
    }
}

public class FeedGrain : Grain
{
    public async Task<List<FeedItem>> ReadFeedAsync(Uri uri, FeedType type, FeedChannel channel)
    {
        var feed = new List<FeedItem>();

        try
        {
            using var xmlReader = XmlReader.Create(uri.ToString(), new XmlReaderSettings() { Async = true });
            if (type == FeedType.Rss)
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
                            feed.Add(new FeedItem(channel, item));
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
                            feed.Add(new FeedItem(channel, entry));
                            break;

                        default:
                            var content = await feedReader.ReadContent();

                            break;
                    }
                }
            }

            return feed;
        }
        catch
        {
            return new List<FeedItem>();
        }
    }
}


public interface IHelloArchive : Orleans.IGrainWithIntegerKey
{
    Task AddReminder(string reminder, TimeSpan repeatEvery);

    Task RemoveReminder(string reminder);

    Task SayHello(string greeting);
}

public class FeedChannel
{
    public string? Title { get; set; }

    public string? Website { get; set; }

    public Uri? Url { get; set; }

    public bool HideTitle { get; set; }

    public bool HideDescription { get; set; }
}

public class FeedItem
{
    public FeedChannel Channel { get; set; }

    public ISyndicationItem Item { get; set; }

    public FeedItem(FeedChannel channel, ISyndicationItem item)
    {
        Channel = channel;
        Item = item;
    }
}

public enum FeedType
{
    Atom,
    Rss
}

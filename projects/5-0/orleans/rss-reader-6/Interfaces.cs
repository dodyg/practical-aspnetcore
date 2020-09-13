
using System.Collections.Generic;
using System.Threading.Tasks;

interface IFeedSource : Orleans.IGrainWithIntegerKey
{
    Task AddAsync(FeedSource source);

    Task<List<FeedSource>> GetAllAsync();

    Task<FeedSource?> FindFeedSourceByUrlAsync(string url);

    Task<FeedSource?> UpdateFeedSourceStatus(string url, bool activeStatus, string? message);
} 

interface IFeedFetcher : Orleans.IGrainWithStringKey
{
    Task FetchAsync(FeedSource source);
}

interface IFeedItemResults : Orleans.IGrainWithIntegerKey
{
    Task AddAsync(List<FeedItem> items);

    Task<List<FeedItem>> GetAllAsync();

    Task ClearAsync();
}

interface IFeedFetcherReminder  : Orleans.IGrainWithStringKey
{
    Task AddReminder(string reminder, short repeatEveryMinute);
}

interface IFeedStreamReader : Orleans.IGrain { }
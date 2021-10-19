using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

await new HostBuilder()
    .UseOrleans(builder =>
    {
        builder
            .UseLocalhostClustering()
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "HelloWorldApp";
            })
            .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
            .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
            .AddRedisGrainStorage("Redis", optionsBuilder => optionsBuilder.Configure(options =>
            {
                options.ConnectionString = "localhost:6379";
                options.UseJson = true;
                options.DatabaseNumber = 1;
            }));
    })
    .ConfigureServices(services =>
    {
        services.Configure<ConsoleLifetimeOptions>(options =>
        {
            options.SuppressStatusMessages = true;
        });
    })
    .ConfigureLogging(builder =>
    {
        builder.AddConsole();
    })
    .RunConsoleAsync();
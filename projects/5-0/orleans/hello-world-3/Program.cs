using System;
using System.Net;
using System.Threading;
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
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "HelloWorldApp";
            })
            .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
            .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
            .AddMemoryGrainStorage(name: "ArchiveStorage");
    })
    .RunConsoleAsync();


class Startup
{
     IHostEnvironment _env;

    public Startup(IHostEnvironment env) 
    {
        _env = env;
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
                IGrainFactory client = context.RequestServices.GetService<IGrainFactory>();
                IHello grain = client.GetGrain<IHello>(0);
                var res = await grain.SayHello("Hello world");
                await context.Response.WriteAsync(res);
            });
        });
    }
}


public class HelloGrain : Grain, IHello
{
    private readonly ILogger _logger;

    public HelloGrain(ILogger<HelloGrain> logger)
    {
        _logger = logger;
    }  

    Task<string> IHello.SayHello(string greeting)
    {
        _logger.LogInformation($"SayHello message received: greeting = '{greeting}'");
        return Task.FromResult($"You said: '{greeting}', I say: Hello!");
    }
}

public class HelloArchiveGrain : Grain, IHelloArchive
{
    private readonly IPersistentState<GreetingArchive> _archive;

    public HelloArchiveGrain([PersistentState("archive", "ArchiveStorage")] IPersistentState<GreetingArchive> archive)
    {
        _archive = archive;
    }

    public async Task<string> SayHello(string greeting)
    {
        _archive.State.Greetings.Add(greeting);

        await _archive.WriteStateAsync();

        return $"You said: '{greeting}', I say: Hello!";
    }

    public Task<IEnumerable<string>> GetGreetings() => Task.FromResult<IEnumerable<string>>(_archive.State.Greetings);
}

public class GreetingArchive
{
    public List<string> Greetings { get; } = new List<string>();
}

public interface IHello : Orleans.IGrainWithIntegerKey
{
    Task<string> SayHello(string greeting);
}

public interface IHelloArchive : Orleans.IGrainWithIntegerKey
{
    Task<string> SayHello(string greeting);

    Task<IEnumerable<string>> GetGreetings();
}
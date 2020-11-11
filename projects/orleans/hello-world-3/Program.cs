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
            .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloArchiveGrain).Assembly).WithReferences())
            .AddMemoryGrainStorage(name: "ArchiveStorage");
    })
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
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
                IGrainFactory client = context.RequestServices.GetService<IGrainFactory>()!;
                IHelloArchive grain = client.GetGrain<IHelloArchive>(0)!;
                await grain.SayHello("Hello world");
                await context.Response.WriteAsync("Keep refreshing your browser \n");
                var res2 = await grain.GetGreetings();
                await context.Response.WriteAsync(string.Join("\n", res2));
            });
        });
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

public interface IHelloArchive : Orleans.IGrainWithIntegerKey
{
    Task<string> SayHello(string greeting);

    Task<IEnumerable<string>> GetGreetings();
}
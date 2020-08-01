using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using System.Collections.Generic;

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

    public HelloArchiveGrain([PersistentState("archive", "Redis")] IPersistentState<GreetingArchive> archive)
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
using System.Security.Cryptography;
using Elsa.Extensions;
using Elsa.Workflows.Core;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Contracts;
using Elsa.Workflows.Core.Memory;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

var magicNumber = new Variable<int>("magic-number", 0);

var workflow = new Sequence
{
    Variables = { magicNumber },
    Activities =
    {
        new GetRandom
        {
            Result = new(magicNumber)
        },
        new WriteLine(ctx =>
        {
            var number = magicNumber.Get(ctx);
            return $"The magic number is { number }";
        })
    }
};

await runner.RunAsync(workflow);

class GetRandom : Composite<int>
{
    private readonly Variable<int> _random = new();

    public GetRandom()
    {
        var random = RandomNumberGenerator.Create();
        var bytes = new byte[sizeof(int)]; 
        random.GetNonZeroBytes(bytes);
        var result = BitConverter.ToInt32(bytes);

        Root = new Sequence
        {
            Variables = { _random },
            Activities = 
            {
                new WriteLine($"{nameof(GetRandom)} compositve is generating random information"),
                new SetVariable<int>(_random,result)
            }
        };
    }

    protected override void OnCompleted(ActivityCompletedContext context)
    {
        var random = _random.Get<int>(context.ChildContext);
        context.ChildContext.Set(Result, random);
    }
}


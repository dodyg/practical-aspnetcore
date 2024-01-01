using Elsa.Extensions;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Contracts;
using Elsa.Workflows.Memory;
using Elsa.Workflows.Models;
using Elsa.Workflows.Services;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

var counter = new Variable<int>("counter", 0);
counter.Value = 1;

var workflow = new Sequence
{
    Variables = { counter },
    Activities =
    {
        new While(context => counter.Get(context) <= 10)
        {
            Body = new Sequence
                {
                    Activities =
                    {
                        new WriteLine(context => $"Counter {counter.Get(context)}"),
                        new SetVariable<int>(counter, context => counter.Get(context) + 1)
                    }
                }
        }
    }
};

await runner.RunAsync(workflow);

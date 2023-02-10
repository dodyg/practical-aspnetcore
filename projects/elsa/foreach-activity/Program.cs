using Elsa.Expressions.Models;
using Elsa.Extensions;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Models;
using Elsa.Workflows.Core.Services;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

var counter = new Variable<int>("current");
var numbers = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

var workflow = new Sequence
{
    Variables = { counter },
    Activities =
    {
        new ForEach(numbers)
        {
            CurrentValue = new Output<object>(counter),
            Body = new Sequence
                {
                    Activities =
                    {
                        new WriteLine(context => $"Counter {counter.Get(context)}")
                    }
                }
        }
    }
};

await runner.RunAsync(workflow);

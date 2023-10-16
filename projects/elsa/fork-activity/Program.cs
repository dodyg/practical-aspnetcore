using Elsa.Extensions;
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
    Activities = 
    {
        new WriteLine("Start workflow before"),
        new Fork {
            Branches = 
            {
                new Sequence 
                {
                    Activities = 
                    {
                        new WriteLine("Branch 1 Step 1"),
                        new WriteLine("Branch 1 Step 2")
                    }
                },
                new Sequence
                {
                    Activities = 
                    {
                        new WriteLine("Branch 2 Step 1"),
                        new WriteLine("Branch 2 Step 2"),
                        new WriteLine("Branch 2 Step 3"),
                    }
                }
            }
        },
        new WriteLine("Finish workflow")
    }
};

await runner.RunAsync(workflow);


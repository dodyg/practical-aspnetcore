using System.Security.Cryptography;
using Elsa.Expressions.Models;
using Elsa.Extensions;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Models;
using Elsa.Workflows.Core.Services;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

var magicNumber = new Variable<int>("magic-number");

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


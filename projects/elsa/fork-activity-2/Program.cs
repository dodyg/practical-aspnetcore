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

var msg1 = new Variable<string>("msg1");
var msg2 = new Variable<string>("msg2");

var workflow = new Sequence
{
    Variables = { msg1, msg2 },
    Activities = 
    {

        new WriteLine("Start workflow before"),
        new Fork {
            JoinMode = ForkJoinMode.WaitAny,
            Branches = 
            {
                new Sequence 
                {
                    Variables = { msg1 },
                    Activities = 
                    {
                        new WriteLine("Branch 1 Step 1"),
                        new SetVariable<string>(msg1, "branch 1")
                    }
                },
                new Sequence
                {
                    Variables = { msg2 },
                    Activities = 
                    {
                        new WriteLine("Branch 2 Step 1"),
                        new SetVariable<string>(msg2, "branch 2")
s                    }
                }
            }
        },
        new WriteLine(ctx => $"""Finish workflow with msg1="{ msg1.Get(ctx)}" and msg2="{ msg2.Get(ctx)}" """)
    }
};

await runner.RunAsync(workflow);


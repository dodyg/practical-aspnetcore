using Elsa.Extensions;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Contracts;
using Elsa.Workflows.Memory;
using Elsa.Workflows.Models;

var services = new ServiceCollection();
services.AddElsa();

var input = new Input<string>("What is your name");
var name = new Variable<string>("name");

var serviceProvider = services.BuildServiceProvider();
var workflow = new Sequence 
{
    Variables = 
    {
        name
    },
    Activities = 
    {
        new WriteLine(context => input.Get(context)),
        new ReadLine(name),
        new WriteLine(ctx => "My name is " + name.Get(ctx))
    }    
};

var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();
await runner.RunAsync(workflow);



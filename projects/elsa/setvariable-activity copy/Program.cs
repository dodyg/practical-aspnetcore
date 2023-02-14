using Elsa.Extensions;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Models;
using Elsa.Workflows.Core.Services;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

var msg = new Variable<string>("message");
msg.Value = "hello";

var workflow = new Sequence
{
    Variables = { msg },
    Activities =
    {
        new WriteLine(context => msg.Get(context)),
        new SetVariable<string>(msg, "hello world"),
        new WriteLine(context => msg.Get(context)),
        new SetVariable<string>(msg, "hello world from Cairo"),
        new WriteLine(context => msg.Get(context)),
    }
};

await runner.RunAsync(workflow);

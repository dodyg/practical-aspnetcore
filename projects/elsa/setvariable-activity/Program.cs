using Elsa.Extensions;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Contracts;
using Elsa.Workflows.Memory;

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
        new WriteLine(msg.Get),
        new SetVariable<string>(msg, "hello world"),
        new WriteLine(msg.Get),
        new SetVariable<string>(msg, "hello world from Cairo"),
        new WriteLine(msg.Get),
    }
};

await runner.RunAsync(workflow);

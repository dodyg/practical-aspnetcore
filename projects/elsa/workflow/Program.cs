using Elsa.Extensions;
using Elsa.Workflows.Core;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Contracts;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var workflow = new MessageWorkflow();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

await runner.RunAsync(workflow);

public class MessageWorkflow : WorkflowBase
{
    protected override void Build(IWorkflowBuilder builder)
    {
        builder.WithVariable<string>("message", "hello workflow");
        builder.Root = new Sequence
        {
            Activities =
            {
                new WriteLine(context => context.GetVariable<string>("message"))
            } 
        };
    }
}

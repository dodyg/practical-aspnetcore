using Elsa.Extensions;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Services;

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
        builder.Name = "Workflow for message";
        builder.Description = "This is a sample worfklow definition";
        builder.WithCustomProperty("workflow", "This is a workflow");
        builder.WithCustomProperty("created on", DateTime.UtcNow);
        builder.Root = new Sequence
        {
            Activities =
            {
                new WriteLine($"Workflow Name: { builder.Name } "),
                new WriteLine($"Worfklow DefinitionId: { builder.DefinitionId }"),
                new WriteLine($"Workflow Description: { builder.Description }"),
                new WriteLine($"Workflow Property[workflow]: { builder.CustomProperties["workflow"] as string }"),
                new WriteLine($"Workflow Property[created on]: { Convert.ToDateTime(builder.CustomProperties["created on"]) }")
            } 
        };
    }
}

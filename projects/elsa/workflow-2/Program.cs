using Elsa.Extensions;
using Elsa.Workflows.Core;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Contracts;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var workflow = new MessageWorkflow();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

var result = await runner.RunAsync(workflow);

Console.WriteLine($"Workflow Name: {result.Workflow.WorkflowMetadata.Name}");
Console.WriteLine($"Worfklow DefinitionId: { result.Workflow.Identity.DefinitionId }");
Console.WriteLine($"Workflow Description: {result.Workflow.WorkflowMetadata.Description}");
Console.WriteLine($"Workflow Property[workflow]: { result.Workflow.CustomProperties["workflow"]}");
Console.WriteLine($"Workflow Property[created on]: { Convert.ToDateTime(result.Workflow.CustomProperties["created on"]) }");

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
            Activities = { }
        };
    }
}

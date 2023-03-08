using Elsa.Extensions;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Models;
using Elsa.Workflows.Core.Services;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

var input = new Dictionary<string, object>{
    ["name"] = "Anne",
    ["age"] = 37
};

var option = new RunWorkflowOptions(Input: input);
await runner.RunAsync<InputWorkflow>(option);

public class InputWorkflow : WorkflowBase
{
    protected override void Build(IWorkflowBuilder builder)
    {
        var nameInput = new Variable<string>();
        var ageInput = new Variable<int>();

        builder.Root = new Sequence
        {
            Variables = { nameInput, ageInput },
            Activities = 
            {
                new SetVariable
                {
                    Variable = nameInput,
                    Value = new Input<object>(ctx => ctx.GetInput<string>("name"))
                },
                new SetVariable
                {
                    Variable = ageInput,
                    Value = new Input<object>(ctx => ctx.GetInput<int>("age"))
                },
                new WriteLine(ctx => $"Name: {nameInput.Get(ctx)}"),
                new WriteLine(ctx => $"Age: {ageInput.Get(ctx)}")
            }
        };
    }
}

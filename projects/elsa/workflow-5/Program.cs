using Elsa.Extensions;
using Elsa.Workflows.Core;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Contracts;
using Elsa.Workflows.Core.Memory;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

await runner.RunAsync(new ConstructorWorkflow("Anne", 37));

public class ConstructorWorkflow : WorkflowBase
{
    private readonly Variable<string> _name;
    private readonly Variable<int> _age;

    public ConstructorWorkflow(string name, int age)
    {
        _name = new Variable<string>(name);
        _age = new Variable<int>(age);
    }

    protected override void Build(IWorkflowBuilder builder)
    {
        builder.Root = new Sequence
        {
            Variables = { _name, _age },
            Activities = 
            {
                new WriteLine(ctx => $"Name: {_name.Get(ctx)}"),
                new WriteLine(ctx => $"Age: {_age.Get(ctx)}")
            }
        };
    }
}

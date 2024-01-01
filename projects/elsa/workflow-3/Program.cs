using System.Security.Cryptography;
using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Contracts;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

var result = await runner.RunAsync<int>(new RandomWorkflow());

Console.WriteLine($"Result {result.Result}");

public class RandomWorkflow : WorkflowBase<int>
{
    protected override void Build(IWorkflowBuilder builder)
    {
        var random = RandomNumberGenerator.Create();
        var bytes = new byte[sizeof(int)]; // 4 bytes
        random.GetNonZeroBytes(bytes);
        var result = BitConverter.ToInt32(bytes);   

        builder.Root = new SetVariable
        {
            Variable = Result,
            Value = new Elsa.Workflows.Models.Input<object>(ctx => result)
        };
    }
}

using Elsa.Extensions;
using Elsa.Workflows.Core.Services;

var services = new ServiceCollection();
services.AddElsa(elsa =>
{
    elsa.AddWorkflow<SayHelloWorkflow>();
});

var serviceProvider = services.BuildServiceProvider();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

await runner.RunAsync<SayHelloWorkflow>();


public class SayHelloWorkflow : IWorkflow
{
    public ValueTask BuildAsync(IWorkflowBuilder builder, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("hello world");
        return ValueTask.CompletedTask;
    }
}
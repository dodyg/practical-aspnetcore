using Elsa.Extensions;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Contracts;
using Elsa.Workflows.Core.Memory;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

var message = new Variable<string>("message");
message.Value = "message";

var workflow = new Sequence{
    Variables = { message },
    Activities =
    {
        new WriteLine("Printing variable name and value"),
        new WriteLine($"Variable name : value = {message.Name} : {message.Value}")
    } 
};

await runner.RunAsync(workflow);

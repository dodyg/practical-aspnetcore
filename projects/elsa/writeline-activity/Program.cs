using Elsa.Extensions;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Services;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var workflow = new WriteLine("Hello world"); //This is an Esla activity
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

await runner.RunAsync(workflow);



using Elsa.Extensions;
using Elsa.Workflows.Core.Services;

var services = new ServiceCollection();
services.AddElsa(elsa =>
{
    elsa.AddWorkflow<SayHelloWorkflow>();
});

var serviceProvider = services.BuildServiceProvider();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

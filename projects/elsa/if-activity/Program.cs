using Elsa.Extensions;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Models;
using Elsa.Workflows.Core.Services;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

var money = new Variable<int>("money");
money.Value = 200;

var workflow = new Sequence{
    Variables = { money },
    Activities =
    {
        new If 
        {
            Condition = new Input<bool>(context => money.Get<int>(context) > 70),
            Then = new WriteLine("You have enough money purchase this Nintendo game"),
            Else = new WriteLine("You don't have enough money to purchase this Nintendo game")            
        }
    } 
};

await runner.RunAsync(workflow);

# ELSA Workflow (14)

ELSA is a workflow engine for .NET Core. It is a library that can be used to build workflow applications. It is also a standalone application that can be used to run workflows.

It is included in this ASP.NET Core samples repository because I believe that a workflow engine can play a very substiantial role in the future of ASP.NET Core applications.

## Activities

- [WriteLine Activity](writeline-activity)
    
    This sample demonstrates a very simple workflow Activity that writes a line to the console.

- [Sequence Activity](sequence-activity)

    This sample demonstrates the `Sequence` activity. The `Sequence` activity is a container activity that contains other activities. The `Sequence` activity executes the activities in the order they are added to the workflow.

- [If Activity](if-activity)

    This sample demonstrates the `If` activity. The `If` activity is a container activity that contains two activities. The first activity is executed if the condition is true. The second activity is executed if the condition is false.

- [SetVariable Activity](setvariable-activity)

    This sample demonstrates the `SetVariable` activity. The `SetVariable` activity is used to set a variable in the workflow.

- [While Activity](while-activity)

    This sample demonstrates the `While` activity.  The `While` activity has a `Condition` property that is used to specify the condition  on when the workflow continues to be executed. 

- [For Activity](for-activity)

    This samples demonstrates the `For` activity. We use `For` activity to execute a workflow in a loop within specified parameters. 

- [ForEach Activity](foreach-activity)

    This samples demonstrates the use of `ForEach` activity. We use `ForEach` activity to execute a workflow in a loop based on a given collection. 

- [Composite Activity](composite-activity)

    Composite Activity is a way to encapsulate a set of activities into a single activity.

- [Fork Activity](fork-activity)

    Fork Activity allows you to split a workflow into two or more branches, each with its own set of actions. All the branches in the workflow must be completed before the worfklow con continue to the next step after the fork.

- [Fork Activity 2](fork-activity-2)

    This sample demonstrates on how to set and use variables in a fork activity.

## Workflow

- [Workflow with variables](workflow)
    
    This sample demonstrates on how to create a custom workflow and pass a variable to it.

- [Workflow custom properties](workflow-2)

    This sample shows how to set up custom properties for a workflow. We also use the basic workflow properties such as `DefinitionId`, `Description`, etc.

- [Workflow that return result](workflow-3)

    This sample demonstrates on how to create a workflow that returns result.

- [Workflow with runtime inputs](workflow-4)

    This sample demonstrates on how to create a workflow that take runtime inputs.
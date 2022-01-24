# View Component (4)

  We are exploring everything about ViewComponent in this section.

  * [ View Component - Hello world](/projects/mvc/view-component/view-component-1)

    This is the simplest sample of a `ViewComponent` that accept parameters. As you can see, the file for the `ViewComponent` class can be located anywhere. 

    From the [doc](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components?view=aspnetcore-2.2#view-search-path) 
    
    > The runtime searches for the view in the following paths:

    > * /Pages/Components/{View Component Name}/{View Name}
    > * /Views/{Controller Name}/Components/{View Component Name}/{View Name}
    > * /Views/Shared/Components/{View Component Name}/{View Name}
    
    > We recommend you name the view file Default.cshtml... 

    So you will find the code for this `HelloWorldViewComponent` at `/Views/Shared/Components/HelloWorld/HelloWorld.cs` and the view at `/Views/Shared/Components/HelloWorld/Default.cshtml`.

  * [ View Component - Alternative Declaration](/projects/mvc/view-component/view-component-2)

    This sample is the same as previous sample except the use of Tag Helper invocation. Use `@addTagHelper *, <AssemblyName>` to enable the invocation of view component as a Tag Helper. Pascal-cased view component class and properties are translated into their lower kebab case. 

  * [ View Component - Return View Component result directly from a controller](/projects/mvc/view-component/view-component-3)

    This sample shows how to return the output of a View Component directly from a controller. Don't forget that your `_Layout.cshtml` won't be used here. It will just return whatever your View Component is producing.

  * [ View Component - Passing complex object as parameter](/projects/mvc/view-component/view-component-4)

    This sample shows you how to pass complex object to the View Component.

    dotnet6
# Blazor (18)

Make sure you installed the latest [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1).

You will find samples for Blazor, a .NET application framework for Web Assembly here. To run the sample, simply type `dotnet watch run` at the folder of each project. Make sure you pay attention which port Kestrel is running on.

  * [Hello World](/projects/blazor/HelloWorld)

    The smallest Blazor app you can create.

  * [Component](/projects/blazor/Component)

    This sample shows the simplest demonstration of a component (which is just a .cshtml file) that accept a string parameter. Notice that the folder name of the project is capitalized. That's due to this [limitation](https://github.com/aspnet/Blazor/issues/854)  e.g. the name of your folder is currently the name of the namespace used in a blazor app.

    A component is represented by a `<componentname></componentname>` in the markup. 

  * [Component Two - Reference and Call](/projects/blazor/ComponentTwo)
  
    This sample shows how to refer to a component using `ref` property and call a public method of the component. We use the method `this.StateHasChanged();` to tell the component that its value has changed.

    Side note: If you are using `ref` without providing corresponding property at `functions`, the compilation will fail.
    
  * [Component Three - Child Content](/projects/blazor/ComponentThree)

    Implement a component that takes one or more child content e.g. `<parentcomponent><childcomponent1/><childcomponent2/><parentcomponent>` and renders it. 

    You have to provide a parameter called `ChildContent` of type `RenderFragment`. The parameter name must be `ChildContent` otherwise it won't work.

  * [Component Four - Handling Custom Event from Component](/projects/blazor/ComponentFour)

    This sample shows how to raise a custom event from a component and how to handle them using `EventCallback<>`.
   
  * [Component Five - Inherit from a ComponentBase class](/projects/blazor/ComponentFive)

    This sample shows how to inherit from a `ComponentBase` class. This allows you to share common code across components. You can also use this technique to have a 'Code Behind' experience if that's your thing.

  * [Component Six - When to call StateHasChanged](/projects/blazor/ComponentSix)

    This sample shows different behaviors by the component depending on where you call the method from. Note: __this is still tentative. It needs more exploration__. 

  * [Component Seven - Plain C#](/projects/blazor/ComponentSeven)

    This sample demonstrates that you can use and organize your C# classes like in an ordinary C# app.

  * [Component Eight - Interactions between two components](/projects/blazor/ComponentEight)

    This samples demonstrates the two way (__currently__) of facilitating interaction between two components.

  * [Component Nine - Data binding from Child Component to Parent](/projects/blazor/ComponentNine)

    This samples demonstrates another way on how to pass data from Child component to Parent using two way data binding. The other way is using Custom Event (see __Component Four__).

  * [Component Ten - Data binding from Child Component to Parent on Collection](/projects/blazor/ComponentTen)

    Similar to __Component Nine__ except that this time the property is a `List<int>` instead of an `int`.

  * [Component Eleven - Capture unmatched component parameters](/projects/blazor/ComponentEleven)

    Use `[Parameter(CaptureUnmatchedValues = true)]` to capture unmatched parameters.

  * [Component Twelve - Splatting arbitrary parameters to components](/projects/blazor/ComponentTwelve)

    Use `@attributes` and a `Dictionary<string, object>` or `List<KeyValuePair<string, object>>`.
    
  * [Component Thirteen - more example of attributes splatting](/projects/blazor/ComponentThirteen)

    Use `@attributes` attributes splatting on an input form.

  * [Component Fourteen - various ways to pass data to components](/projects/blazor/ComponentFourteen)

    This sample demonstrates the various ways to pass parameters to a component and how it affects on how the data is perceived by the component.

  * [Component Fifteen](/projects/blazor/ComponentFifteen)

    This sample demonstrates how to use partial class in a razor component. This allows you to separate your C# code from the markup.
    
  * [Data Binding - Form](/projects/blazor/DataBinding)

    Show an example of two day databinding to form element `input=text`, `textarea`. `input=checkbox`, and `select`.

  * [Date Binding - EditForm](/projects/blazor/DataBindingTwo)

    Show an example of `EditForm` and its 6 input controls, including form validation.

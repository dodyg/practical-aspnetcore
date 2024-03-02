# Blazor Wasm (22)

You will find samples for Blazor, a .NET application framework for Web Assembly here. To run the sample, simply type `dotnet watch run` at the folder of each project. Make sure you pay attention which port Kestrel is running on.

  * [Hello World](HelloWorld)

    The smallest Blazor app you can create.

  * [Component](Component)

    This sample shows the simplest demonstration of a component (which is just a .cshtml file) that accept a string parameter. Notice that the folder name of the project is capitalized. That's due to this [limitation](https://github.com/aspnet/Blazor/issues/854)  e.g. the name of your folder is currently the name of the namespace used in a blazor app.

    A component is represented by a `<componentname></componentname>` in the markup. 

  * [Component Two - Reference and Call](ComponentTwo)
  
    This sample shows how to refer to a component using `ref` property and call a public method of the component. We use the method `this.StateHasChanged();` to tell the component that its value has changed.

    Side note: If you are using `ref` without providing corresponding property at `functions`, the compilation will fail.
    
  * [Component Three - Child Content](ComponentThree)

    Implement a component that takes one or more child content e.g. `<parentcomponent><childcomponent1/><childcomponent2/><parentcomponent>` and renders it. 

    You have to provide a parameter called `ChildContent` of type `RenderFragment`. The parameter name must be `ChildContent` otherwise it won't work.

  * [Component Four - Handling Custom Event from Component](ComponentFour)

    This sample shows how to raise a custom event from a component and how to handle them using `EventCallback<>`.
   
  * [Component Five - Inherit from a ComponentBase class](ComponentFive)

    This sample shows how to inherit from a `ComponentBase` class. This allows you to share common code across components. You can also use this technique to have a 'Code Behind' experience if that's your thing.

  * [Component Six - When to call StateHasChanged](ComponentSix)

    This sample shows different behaviors by the component depending on where you call the method from. Note: __this is still tentative. It needs more exploration__. 

  * [Component Seven - Plain C#](ComponentSeven)

    This sample demonstrates that you can use and organize your C# classes like in an ordinary C# app.

  * [Component Eight - Interactions between two components](ComponentEight)

    This samples demonstrates the two way (__currently__) of facilitating interaction between two components.

  * [Component Nine - Data binding from Child Component to Parent](ComponentNine)

    This samples demonstrates another way on how to pass data from Child component to Parent using two way data binding. The other way is using Custom Event (see __Component Four__).

  * [Component Ten - Data binding from Child Component to Parent on Collection](ComponentTen)

    Similar to __Component Nine__ except that this time the property is a `List<int>` instead of an `int`.

  * [Component Eleven - Capture unmatched component parameters](ComponentEleven)

    Use `[Parameter(CaptureUnmatchedValues = true)]` to capture unmatched parameters.

  * [Component Twelve - Splatting arbitrary parameters to components](ComponentTwelve)

    Use `@attributes` and a `Dictionary<string, object>` or `List<KeyValuePair<string, object>>`.
    
  * [Component Thirteen - more example of attributes splatting](ComponentThirteen)

    Use `@attributes` attributes splatting on an input form.

  * [Component Fourteen - various ways to pass data to components](ComponentFourteen)

    This sample demonstrates the various ways to pass parameters to a component and how it affects on how the data is perceived by the component.

  * [Component Fifteen](ComponentFifteen)

    This sample demonstrates how to use partial class in a razor component. This allows you to separate your C# code from the markup.
    
  * [Component Sixteen](ComponentSixteen)

    This sample demonstrates cascading value by type feature. This is a parameter that get passed through a component without having to explicitly assign them. All parameters that share the type will share the value.
    
  * [Component Seventeen](ComponentSeventeen)

    This sample demonstrates cascading value by name feature. In contrast to cascading value by type, only parameters that match the type and name will receive the value.

  * [Component Eighteen](ComponentEighteen) 

    This sample demonstrates @bind:after modifier that allows to execute async code after a binding event has been completed (value has changed).

  * [Component Nineteen](ComponentNineteen) 

    This sample demonstrates @bind:get @bind:set modifier that simplify two-way data binding. 
    
  * [Component Twenty](ComponentTwenty)

    This sample shows how to implement a HTML custom element using Blazor Web Assembly.

  * [Component Twenty One](ComponentTwentyOne)

    This sample shows how set and get a property of a HTML custom element implemented using Blazor Web Assembly.

  * [Component Twenty Two](ComponentTwentyTwo)

    This sample shows how to raise an event from HTML custom element implemented using Blazor Web Assembly.

  * [ComponentTwentyThree](ComponentTwentyThree)

    This sample shows how to set root level cascading values without using `<CascadingValue/>` component. 
  
  * [ComponentTwentyFour](ComponentTwentyFour)

    This sample shows how to set root level **named** cascading values without using `<CascadingValue/>` component. 

  * [ComponentTwentyFive](ComponentTwentyFive)

    This sample shows how to set root level **dynamic** cascading values using `CascadingValueSource`.

  * [ComponentTwentySix](ComponentTwentySix)

    This sample shows how to set root level **named dynamic** cascading values using `CascadingValueSource`.

  * [ComponentTwentySeven](ComponentTwentySeven)

    This sample shows how to use `[Inject(Key)]` in consuming keyed services.

  * [Data Binding - Form](DataBinding)

    Show an example of two day databinding to form element `input=text`, `textarea`. `input=checkbox`, and `select`.

  * [Date Binding - EditForm](DataBindingTwo)

    Show an example of `EditForm` and its 6 input controls, including form validation.

  * [RenderFragment](RenderFragment)

    Show an example of `RenderFragment` or show an example of Templated component.

  * [RadioButton](RadioButton)

    Show an example of `RadioButton` handling.

* [QuickGrid One](QuickGridOne)
  
  This sample demonstrates a simple usage of QuickGrid component displaying int, string, date, and boolean data types.

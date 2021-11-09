
using Microsoft.AspNetCore.Components;

class Separator : ComponentBase
{
    protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "hr");
        builder.CloseElement(); // hr
    }
}

class ListNames : ComponentBase
{
    protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
    {
        // see the following:
        // https://docs.microsoft.com/en-us/aspnet/core/blazor/advanced-scenarios

        builder.OpenElement(0, "ul");
        var names = new string[]
        {
            "Bruce",
            "Clint",
            "Donald",
            "Natasha",
            "Steve",
            "Tony",
        };
        foreach (var x in names)
        {
            builder.OpenElement(1, "li");
            builder.OpenElement(2, "p");
            builder.AddContent(3, x);
            builder.CloseElement(); // p
            builder.CloseElement(); // li
        }
        builder.CloseElement(); // ul

        // shows an example of a nested component
        builder.OpenComponent<Separator>(4);
        builder.CloseComponent();
    }
}


# RadioButton

`RadioButton` demo shows how to handle Radio button selection in Blazor.

``` csharp
@foreach (var item in new string[] { "AspNetCore", "AspNet", "SomeJsThingWhatever" })
{
    <div>
        <input type="radio" name="technology" id="@item" value="@item" @onchange="TechnologyRadioSelection" checked=@(TechnologyChoice.Equals(item,StringComparison.OrdinalIgnoreCase)) />
        <label for="@item">@item</label>
    </div>
}

<div>
    <label>Technology selected is <b>@TechnologyChoice</b></label>
</div>

@code
{
    string TechnologyChoice = "aspnetcore";
    void TechnologyRadioSelection(ChangeEventArgs args)
    {
        TechnologyChoice = args.Value.ToString();
    }
}
```

Contribution by [Lohit](https://github.com/lohithgn).
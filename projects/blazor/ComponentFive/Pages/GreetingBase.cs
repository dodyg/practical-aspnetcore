
using Microsoft.AspNetCore.Blazor.Components;
using System;

public class GreetingBase : BlazorComponent
{
    [Parameter]
    Action<int> OnUpdate { get; set; }

    int _currentCount;

    protected void IncrementCount()
    {
        _currentCount++;
        if (OnUpdate != null)
        {
            OnUpdate(_currentCount);
        }
    }
}
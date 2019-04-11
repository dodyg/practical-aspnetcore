
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

public class GreetingBase : ComponentBase
{
    [Parameter]
    EventCallback<int> OnUpdate { get; set; }

    int _currentCount;

    protected async Task IncrementCount()
    {
        _currentCount++;
        await OnUpdate.InvokeAsync(_currentCount);
    }
}
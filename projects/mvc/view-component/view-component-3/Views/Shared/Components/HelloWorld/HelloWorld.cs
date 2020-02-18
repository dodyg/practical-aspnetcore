using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class Greeting
{
    public string Message { get; set; }

    public int Repeat { get; set; }
}

public class HelloWorldViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string message, int times)
    {
        return View(new Greeting { Message = message, Repeat = times });
    }
}
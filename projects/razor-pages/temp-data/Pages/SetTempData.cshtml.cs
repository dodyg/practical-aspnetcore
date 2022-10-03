using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class SetTempDataModel : PageModel
{
    [TempData]
    public string Message { get; set; } = string.Empty;
    
    public IActionResult OnGet()
    {
        Message = "Greeting from SetTempData page";
        return Page();
    }
}
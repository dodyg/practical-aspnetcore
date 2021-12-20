using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesBasic.Pages;

public class SeparateCodebehindFileModel : PageModel
{
    public string Title => "Page with separate codebehind file model";
    public string Message { get; private set; }

    public void OnGet()
    {
        Message = $"Generated at { DateTime.Now.ToLongTimeString() }.";
    }
}

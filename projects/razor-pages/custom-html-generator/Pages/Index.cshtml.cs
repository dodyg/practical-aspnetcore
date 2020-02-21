
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracticalAspNetCore.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public PersonInput Input { get; set; }
        public void OnGet()
        {

        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewRouting
{
    [Message(ContentGet = "This message only shows up on GET", ContentPost = "This message only shows up on POST")]
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            Message = HttpContext.Items["GreetingFromMiddleWare"] as string;
        }

        public async Task OnPostAsync()
        {
            Message = HttpContext.Items["GreetingFromMiddleWare"] as string;
        }
    }
}

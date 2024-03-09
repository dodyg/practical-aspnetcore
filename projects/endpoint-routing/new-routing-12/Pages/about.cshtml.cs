
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracticalAspNetCore
{
    [Message(ContentGet = "This message only shows up on GET", ContentPost = "This message only shows up on POST")]
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = HttpContext.Items["GreetingFromMiddleWare"] as string;
        }

        public void OnPost()
        {
            Message = HttpContext.Items["GreetingFromMiddleWare"] as string;
        }
    }
}


using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewRouting
{
    [Message(Content = "Hello world message from attribute")]
    public class AboutModel : PageModel
    {
        public async Task OnGetAsync()
        {

        }
    }
}

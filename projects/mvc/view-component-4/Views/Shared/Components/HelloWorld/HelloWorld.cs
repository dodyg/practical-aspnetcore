using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace StartupBasic 
{
    public class Greeting
    {
        public string Message { get; set; }

        public int Repeat { get; set; }
    }

    public class HelloWorldViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(RepeatMessage message)
        {
            return View(new Greeting { Message = message.Content, Repeat = message.Repeat });
        }
}
}
using Microsoft.AspNetCore.Mvc;

namespace XmlValidation.ViewComponents
{
    public class SuccessAlertViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string text)
        {
            ViewData["AlertText"] = text;
            return View();
        }
    }
}

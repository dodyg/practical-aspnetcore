using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace XmlValidation.ViewComponents
{
    public class DangerAlertViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string text, IEnumerable<string> errors)
        {
            ViewData["AlertText"] = text;
            ViewData["Errors"] = errors;
            return View();
        }
    }
}

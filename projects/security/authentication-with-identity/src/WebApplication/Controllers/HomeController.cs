using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
			=> View();
	}
}
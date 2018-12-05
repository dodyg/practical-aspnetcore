using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using WebApplication.ViewModels.RestrictedAreaViewModels;

namespace WebApplication.Controllers
{
    [Authorize]
	public class RestrictedAreaController : Controller
	{
        private readonly UserManager<IdentityUser> userManager;
		
		public RestrictedAreaController(UserManager<IdentityUser> userManager)
		{
			this.userManager = userManager;
		}
		
        public async Task<IActionResult> Index()
        {
            IdentityUser user = await this.userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");
            }

            IndexViewModel model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed
            };

            return View(model);
        }
	}
}
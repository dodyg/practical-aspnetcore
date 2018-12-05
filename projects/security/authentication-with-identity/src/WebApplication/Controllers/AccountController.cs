using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApplication.Services;
using System.Text.Encodings.Web;
using System;

namespace WebApplication.Controllers
{
    [Authorize]
	public class AccountController : Controller
	{
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IEmailSender emailSender;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
			IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
			this.emailSender = emailSender;
        }
		
		[HttpGet]
        [AllowAnonymous]
		public IActionResult Register()
			=> View(new RegisterViewModel());
			
		[HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Register([FromForm]RegisterViewModel model)
		{
			if (ModelState.IsValid)
            {
				IdentityUser user = new IdentityUser { UserName = model.Email, Email = model.Email };
                IdentityResult result = await this.userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    string code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
					string callbackUrl = Url.Action(action: nameof(AccountController.ConfirmEmail),controller: "Account",
						values: new { user.Id, code },protocol: Request.Scheme);
                    await this.emailSender.SendEmailAsync(model.Email, "Confirm your email",
						$"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a>");

                    await this.signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction(nameof(HomeController.Index), "Home");
                }
			}
			
			return View(model);
		}

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
			=> View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            IdentityUser user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            IdentityResult result = await this.userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
	}
}
using System.ComponentModel.DataAnnotations;

namespace WebApplication.ViewModels.RestrictedAreaViewModels
{
	public sealed class IndexViewModel
	{
		public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Display(Name = "Email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
	}
}
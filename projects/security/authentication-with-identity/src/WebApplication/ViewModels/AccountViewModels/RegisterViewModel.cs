using System.ComponentModel.DataAnnotations;

namespace WebApplication.ViewModels.AccountViewModels
{
	public class RegisterViewModel
	{
        [Required]
        [EmailAddress]
		public string Email {get;set;}
		
        [Required]
        [DataType(DataType.Password)]
		public string Password {get;set;}
	}
}
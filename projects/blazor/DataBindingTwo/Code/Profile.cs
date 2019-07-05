using System.ComponentModel.DataAnnotations;
using System;

namespace DataBindingTwo.Code
{
    public class Profile
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public bool IsMarried { get; set; }
    }
}
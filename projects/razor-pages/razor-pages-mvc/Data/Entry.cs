using System.ComponentModel.DataAnnotations;

namespace PracticalAspNetCore.Data
{
    public class Entry
    {
        public int Id { get; set; }

        [Required, StringLength(300)]
        public string Content { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public int Likes { get; set; }
    }
}

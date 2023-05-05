using System.ComponentModel.DataAnnotations;

namespace Blogs.Models
{
    public class RegisterInputModel
    {
        [StringLength(16, MinimumLength = 3)]
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password")]
        public string PasswordAgain { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;
    }
}

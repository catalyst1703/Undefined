using System.ComponentModel.DataAnnotations;

namespace Blogs.Models
{
    public class LoginInputModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;

        public string ErrorMessage { get; set; } = string.Empty;
    }
}

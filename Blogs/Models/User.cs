using Microsoft.AspNetCore.Identity;

namespace Blogs.Models
{
    public class User : IdentityUser
    {
        public virtual IEnumerable<Blog> Blogs { get; set; }
    }
}

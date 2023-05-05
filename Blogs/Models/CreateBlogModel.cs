using System.ComponentModel.DataAnnotations;

namespace Blogs.Models
{
    public class CreateBlogModel
    {
        [StringLength(maximumLength: 50, MinimumLength = 5)]
        public string Title { get; set; }

        [StringLength(maximumLength: 300, MinimumLength = 5)]
        public string Description { get; set; }

        public string Body { get; set; }
    }
}

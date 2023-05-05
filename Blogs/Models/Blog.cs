using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Blogs.Models
{ 
    public class Blog
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }
        public virtual User Author { get; set; }
    }
}

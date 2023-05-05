using Blogs.Data;
using Blogs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Blogs.Controllers
{
    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public BlogsController(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            _context = applicationDbContext;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_context.Blog.Include(b => b.Author));
        }

        public IActionResult Blog(string id)
        {
            return View(_context.Blog.Where(blog => blog.Id == id).ToList()[0]);
        }

        [Authorize]
        public IActionResult CreateBlog() 
        {
            return View(new CreateBlogModel());
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogModel createBlogModel)
        {
            if (ModelState.IsValid)
            {
                Blog blog = new Blog()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = createBlogModel.Title,
                    Description = createBlogModel.Description,
                    Body = createBlogModel.Body
                };
                var user = await _userManager.GetUserAsync(User);
                blog.Author = user;
                _context.Blog.Add(blog);
                await _context.SaveChangesAsync();
                return Redirect("/Blogs/");
            }
            return View(createBlogModel);
        }

        [Authorize]
        public async Task<IActionResult> DeleteBlog(string id)
        {
            _context.Blog.Remove(_context.Blog.Where(b => b.Id == id).ToList()[0]);
            await _context.SaveChangesAsync();  
            return Redirect("/Blogs/");
        }
    }
}

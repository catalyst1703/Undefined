using Blogs.Data;
using Blogs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Blogs
{
    public static class  DataBaseSeeder
    {
        public static async Task Initilize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin")) 
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            User adminUser = await userManager.FindByNameAsync("Admin");
            if (adminUser == null) 
            {
                adminUser = new User()
                {
                    UserName = "vlados17",
                    Email = "vladsoroka2005@gmail.com",
                    EmailConfirmed = true
                };
                PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "ZaDrOt666");
                var result = await userManager.CreateAsync(adminUser);
                if(result.Succeeded) 
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}

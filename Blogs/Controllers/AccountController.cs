using Microsoft.AspNetCore.Mvc;
using Blogs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blogs.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Blogs.Models;

namespace Blogs.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            _userManager= userManager;
            _signInManager= signInManager;
            _emailSender= emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View(new LoginInputModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel loginInputModel)
        { 
            User user = await _userManager.FindByEmailAsync(loginInputModel.Email);
            if (user == null)
            {
                return View(new LoginInputModel() { ErrorMessage = "User not found" });
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginInputModel.Password, loginInputModel.RememberMe, false);
            if (result.Succeeded) 
            {
                return Redirect("/Home/");
            }
            return View(new LoginInputModel() { ErrorMessage = "Wrong password"});
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home/");
        }

        public IActionResult Register()
        {
            return View(new RegisterInputModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterInputModel registerInputModel)
        {
            if (ModelState.IsValid)
            {
                User foundedUserByEmail = await _userManager.FindByEmailAsync(registerInputModel.Email)!;
                if (foundedUserByEmail is not null)
                {
                    return View(new RegisterInputModel() { ErrorMessage = $"User with email {registerInputModel.Email} is already exists" });
                }
                User foundedUserByUsername = await _userManager.FindByNameAsync(registerInputModel.Username)!;
                if(foundedUserByUsername is not null) 
                {
                    return View(new RegisterInputModel() { ErrorMessage = $"Username {registerInputModel.Username} is already taken" });
                }
                User user = new User()
                {
                    Email = registerInputModel.Email,
                    UserName = registerInputModel.Username
                };
                user.PasswordHash = new PasswordHasher<User>().HashPassword(user, registerInputModel.Password);
                await _userManager.CreateAsync(user);
                return ConfirmEmailInfo(registerInputModel.Email, user.Id).Result;
            }
            return BadRequest();
        }

        public async Task<IActionResult> ConfirmEmailInfo(string email,string id)
        {
            await _emailSender.SendEmailAsync(email, "Confirmation", $"""<a href = "https://localhost:7195/Account/ConfirmEmail?id={id}">Confirm your email</a>""");
            return View("ConfirmEmailInfo", email);
        }

        public async Task<IActionResult> ConfirmEmail(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            return View(user);
        }
    }
}

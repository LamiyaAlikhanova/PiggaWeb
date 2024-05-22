using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pigga_WebApplication.Models;
using Pigga_WebApplication.ViewModels.Account;

namespace Pigga_WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User>userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm) 
        {
            if(!ModelState.IsValid) return View();

            User newUser = new User()
            {
                Name = registerVm.Name,
                Email = registerVm.Email,
                Surname = registerVm.Surname,
                UserName = registerVm.UserName,
            };

            var result= await userManager.CreateAsync(newUser,registerVm.Password);

            if (result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
                return View();
            }

             
            return RedirectToAction("Login");
        }
        public IActionResult Login ()
        { 
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid) return View(); 
            var user= await userManager.FindByNameAsync(loginVm.UserName);
            if (user == null)
            {
                ModelState.AddModelError(" ", "Username ve ya Pasword yanlisdir");
                return View();
            }
            var result= await signInManager.CheckPasswordSignInAsync(user, loginVm.Password,true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Birazdan gelersiz");
                return View();
                
            }
            if(!result.Succeeded)
            {
                ModelState.AddModelError(" ", "Username ve ya Pasword yanlisdir");
                return View();
            }
            await signInManager.SignInAsync(user, loginVm.RememberMe);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult>LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

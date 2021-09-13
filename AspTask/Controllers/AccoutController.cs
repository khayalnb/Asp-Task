using AspTask.Models;
using AspTask.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspTask.Controllers
{
    public class AccoutController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public object Username { get; private set; }

        public AccoutController(UserManager<AppUser>userManager,SignInManager<AppUser>signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);

            AppUser user = new AppUser
            {
                UserName = register.UserName,
                FullName = register.UserName,
                Email = register.Email
            };

            IdentityResult result = await userManager.CreateAsync(user,register.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
                return View(register);
            }
            return RedirectToAction("Index", "Home");
        }



        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View(login);

            AppUser user = await userManager.FindByNameAsync(login.UserName);

            if(user == null)
            {
                ModelState.AddModelError("","Username or password is not correct" );
                return View(login);
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user,login.Password,false,false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is not correct");
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Test()
        {
            return Content(User.Identity.IsAuthenticated.ToString());
        }
    }
}

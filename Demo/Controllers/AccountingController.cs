using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class AccountingController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountingController(UserManager<IdentityUser> UserManager, SignInManager<IdentityUser> SignInManager)
        {
            userManager = UserManager;
            signInManager = SignInManager;
        }


        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public async   Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
   
        Microsoft.AspNetCore.Identity.SignInResult result= 
        await signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);
      

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(login.ReturnUrl))
                    {
                        return Redirect(login.ReturnUrl);
                    }
                    return RedirectToAction("index", "home");

                }
                ViewBag.login = "نام کاربری یا رمز عبور اشتباه است";
            }
            return View(login);
        }



        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser()
                {
                    Email = userViewModel.Email,
                    UserName = userViewModel.Email
                };
                IdentityResult result=await userManager.CreateAsync(user, userViewModel.Password);
                await userManager.AddToRoleAsync(user, "user");
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("identiuty",item.Description);
                }
                
            }
            return View(userViewModel);

        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        public IActionResult AccessDeny()
        {

            return Content("dir oomady nakha zood bro");
        }
    }
}

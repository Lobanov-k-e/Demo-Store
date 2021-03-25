using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportStore.Infrastructure.Authorization;
using SportStore.WebUi.ViewModels;

namespace SportStore.WebUi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(IUserService userService, SignInManager<IdentityUser> signInManager)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            SeedIdentity.CreateSuperUser(userService).Wait();
        }
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl}); 
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetByUsername(model.UserName);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                        return Redirect(model?.ReturnUrl ?? "/Admin");
                }
                ModelState.AddModelError("", "Incorrect Login or Password");              
            }
            return View(model);
        }
    }
}
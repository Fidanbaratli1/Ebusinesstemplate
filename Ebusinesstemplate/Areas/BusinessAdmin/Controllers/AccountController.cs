using Ebusinesstemplate.Models;
using Ebusinesstemplate.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ebusinesstemplate.Areas.BusinessAdmin.Controllers
{

    [Area("BusinessAdmin")]
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController( UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                Surname = registerVM.Surname,
                UserName = registerVM.UserName,
                Gender = registerVM.Gender,
            };
            IdentityResult result= await _userManager.CreateAsync(user,registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                    return View();
                }

            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home", new {Area=""} );
            

        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            AppUser existed = await _userManager.FindByEmailAsync(loginVM.EmailOrusername);
            if(existed == null)
            {
                existed = await _userManager.FindByNameAsync(loginVM.EmailOrusername);
            }
            var result=await _signInManager.PasswordSignInAsync(existed, loginVM.Password,loginVM.IsRemember,true);
            if(!result.Succeeded)
            {
                return View();
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        
    }
}

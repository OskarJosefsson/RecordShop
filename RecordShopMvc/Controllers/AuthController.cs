using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecordShopClassLibrary.Models.Create;
using RecordShopClassLibrary.Models.Read;
using RecordShopMvc.Models.Entities;

namespace RecordShopMvc.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;


        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult SignIn(string returnUrl = null)
        {
            var viewModel = new SignInViewModel();
            if (returnUrl == null)
                viewModel.ReturnUrl = "/";
            else
                viewModel.ReturnUrl = returnUrl;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                    if (model.ReturnUrl == null || model.ReturnUrl == "/")
                        return RedirectToAction("Products", "Home");
                    else
                        return LocalRedirect(model.ReturnUrl);
            }
            ModelState.AddModelError("", "Incorrect email address or password");
            return View(model);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,

                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Products", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();

                return RedirectToAction("Products", "Home");
            }

            return View();
        }


    }
}

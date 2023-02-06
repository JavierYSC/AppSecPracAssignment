using AppSecAssignment.ViewModels;
using AppSecAssignment.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AppSecPracAssignment.Service;

namespace AppSecAssignment.Pages
{
    public class LoginModel : PageModel
    {
		private readonly GoogleCaptchaService _captchaService;

        [BindProperty]
        public Login LModel { get; set; }

        private UserManager<AppUser> userManager { get; }
		private SignInManager<AppUser> signInManager { get; }
        public LoginModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, GoogleCaptchaService captchaService)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			_captchaService= captchaService;
		}

		public void OnGet()
        {
        }

		public async Task<IActionResult> OnPostAsync()
		{
			var captchaResult = await _captchaService.VerifyToken(LModel.Token);
			if (!captchaResult)
			{
				return Page();
			}
			if (ModelState.IsValid)
			{
				var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, false, lockoutOnFailure:true);
				
				 if (identityResult.Succeeded)
				{
					return RedirectToPage("Index");
				}
				if (identityResult.IsLockedOut)
				{
					ModelState.AddModelError("", "The account is locked out for 30 seconds. ");
					return Page();
				}
				else
				{
					ModelState.AddModelError("", "Invalid username or password.");
				}

			}
			return Page();
		}
	}
}

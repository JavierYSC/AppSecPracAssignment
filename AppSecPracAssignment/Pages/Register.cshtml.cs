using AppSecAssignment.ViewModels;
using AppSecAssignment.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppSecAssignment.Pages
{
    public class RegisterModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }

		[BindProperty]
		public Register RModel { get; set; }

		[BindProperty]
		public IFormFile? Upload { get; set; }

		public RegisterModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _environment = environment;
        }

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
        {
            var imageString = "";
            if (ModelState.IsValid)
            {
				if (Upload != null)
				{
					if (Upload.Length > 2 * 1024 * 1024)
					{
						ModelState.AddModelError("Upload", "File Size cannot exceed 2MB.");
						return Page();
					}

					var uploadsFolder = "uploads";
					var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
					var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
					using var fileStream = new FileStream(imagePath, FileMode.Create);
					await Upload.CopyToAsync(fileStream);
					imageString = string.Format("/{0}/{1}", uploadsFolder, imageFile);
				}

				var user = new AppUser()
                {
                    UserName = RModel.Email,
                    Email = RModel.Email,
                    FName = RModel.Name,
                    CreditCard = RModel.CreditCard,
                    Gender = RModel.Gender,
                    MobileNo = RModel.MobileNumber,
                    Country = RModel.Country,
                    Street = RModel.Street,
                    PostalCode = RModel.PostalCode,
                    Photo = imageString,
                    AboutMe = RModel.AboutMe

                };
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
			}
            return Page();
        }

	}
}

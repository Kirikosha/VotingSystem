using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityVotingSystem.Models;
using UniversityVotingSystem.ViewModels;

namespace UniversityVotingSystem.webpages
{
    public class RegistrationModel : PageModel
    {
        UserManager<User> _userManager;
        public RegistrationModel(UserManager<User> userManager) 
        {
            _userManager = userManager;
        }
        public IActionResult OnGet()
        {
            return new PageResult();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    first_name = Request.Form["first_name"],
                    second_name = Request.Form["last_name"],
                    phone_number = Request.Form["phone_number"],
                    Email = Request.Form["email"],
                    UserName = Request.Form["first_name"] + "_" + Request.Form["last_name"],
                    EmailConfirmed = true,
                    role = "SimpleUser"
                };


                if (await _userManager.FindByEmailAsync(user.Email) != null)
                {
                    return Page();
                }


                string password = Request.Form["password"];
                if (string.IsNullOrEmpty(password))
                {
                    return new PageResult();
                }


                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(user, "SimpleUser");
                }


                return RedirectToPage("/MainPage");
            }
            else
            {
                return new PageResult();
            }
        }

        
    }
}

using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityVotingSystem.Models;
using UniversityVotingSystem.ViewModels;

namespace UniversityVotingSystem.webpages
{
    public class RegistrationModel : PageModel
    {
        private UserManager<User> _userManager;
        private readonly ILogger _logger;
        public RegistrationModel(UserManager<User> userManager, ILogger<RegistrationModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public IActionResult OnGet()
        {
            return new PageResult();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                (User, string) response = CreateUserInstance();
                if (!response.Item2.Equals("OK"))
                {
                    throw new ArgumentNullException(nameof(response.Item1), response.Item2);
                }

                string password = ValidatePassword();
                var result = await _userManager.CreateAsync(response.Item1, password);
                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(response.Item1, "SimpleUser");
                }

                return RedirectToPage("/MainPage");
            }
            else
            {
                return new PageResult();
            }
        }


        private (User, string) CreateUserInstance()
        {
                if(string.IsNullOrEmpty(Request.Form["first_name"]) || string.IsNullOrEmpty(Request.Form["last_name"])
                || string.IsNullOrEmpty(Request.Form["email"]))
                {
                    return (new User(), "Some info was not passed");
                }
                User user = new User()
                {
                    first_name = Request.Form["first_name"].ToString(), //required
                    second_name = Request.Form["last_name"].ToString(), //required
                    phone_number = Request.Form["phone_number"].ToString(), //NOT - required
                    Email = Request.Form["email"].ToString(), //required
                    UserName = Request.Form["first_name"].ToString() + "_" + Request.Form["last_name"].ToString(), //required
                    EmailConfirmed = true,
                    role = "SimpleUser"
                };

                return (user, "OK");
        }

        private string ValidatePassword()
        {
            string? password = Request.Form["password"].ToString();
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), "The password is null");
            }

            return password;
        }
    }
}

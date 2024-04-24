using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityVotingSystem.Models;

namespace UniversityVotingSystem.webpages
{
    public class ProfileModel : PageModel
    {
        UserManager<User> _userManager;

        public User _user {  get; set; }
        public ProfileModel(UserManager<User> userManager)
        {
            _userManager = userManager;
            _user = new User();
        }
        public  IActionResult OnGet()
        {
            IIdentity? identity = HttpContext.User.Identity;
            if (identity is null)
            {
                throw new ArgumentNullException(nameof(identity), "HttpContext.User.Identity was null");
            }

            if (!identity.IsAuthenticated)
            {
                return new RedirectToPageResult("Login");
            }

            if(identity.Name is not null)
            {
                _user = FindUser(identity.Name).Result;
            }

            return new PageResult();
        }

        private async Task<User> FindUser(string username)
        {
            User? user =  await _userManager.FindByNameAsync(username);
            if(user is null)
            {
                throw new ArgumentNullException(nameof(user), "User was not found");
            }

            return user;
        }
    }
}

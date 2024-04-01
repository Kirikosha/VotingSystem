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
        }
        public async Task<IActionResult> OnGet()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return new RedirectToPageResult("Login");
            }
            _user = FindUser(HttpContext.User.Identity.Name).Result;
            return new PageResult();
        }

        private async Task<User> FindUser(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
    }
}

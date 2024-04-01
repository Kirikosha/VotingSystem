using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using UniversityVotingSystem.Models;

namespace UniversityVotingSystem.webpages
{
    public class LoginModel : PageModel
    {
        UserManager<User> _userManager;
        public LoginModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult OnGet()
        {
            return new PageResult();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByEmailAsync(Request.Form["email"]);
            if(user != null && await _userManager.CheckPasswordAsync(user, Request.Form["password"]))
            {
                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));

                return new RedirectToPageResult("MainPage");
            }
            else
            {
                return new PageResult();
            }
        }
    }
}

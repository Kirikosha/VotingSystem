using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using UniversityVotingSystem.Models;

namespace UniversityVotingSystem.webpages
{
    public class LoginPageModel : PageModel
    {
        UserManager<User> _userManager;
        public LoginPageModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult OnGet()
        {
            return new PageResult();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            string? email = Request.Form["email"];
            string? password = Request.Form["password"];
            if (email is null || password is null)
            {
                throw new ArgumentNullException(nameof(email), "Neither email or password were null");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if(user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                var userName = user.UserName;
                if(userName is null)
                {
                    throw new ArgumentNullException(nameof(userName), "Problem with current user, the username is null.");
                }

                identity.AddClaim(new Claim(ClaimTypes.Name, userName));

                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));

                return LocalRedirect("/MainPage");
            }
            else
            {
                return new PageResult();
            }
        }
    }
}

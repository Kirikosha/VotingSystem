using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Principal;
using System.Web;

namespace UniversityVotingSystem.webpages
{
    [AllowAnonymous]
    public class MainPageModel : PageModel
    {
        public IActionResult OnGet()
        {
            IIdentity? identity = HttpContext.User.Identity;
            if (identity is null)
            {
                throw new ArgumentNullException(nameof(identity), "Identity was null, problem with authentication");
            }

            bool userstatus = identity.IsAuthenticated;
            if (!userstatus)
            {
                return new RedirectToPageResult("Login");
            }

            return new PageResult();
        }
    }
}

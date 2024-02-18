using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;

namespace UniversityVotingSystem.webpages
{
    [AllowAnonymous]
    public class MainPageModel : PageModel
    {
        public IActionResult OnGet()
        {
            bool userstatus = HttpContext.User.Identity.IsAuthenticated;
            if(!userstatus)
            {
                return new RedirectToPageResult("Login");
            }
            return new PageResult();
        }
    }
}

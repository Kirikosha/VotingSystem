using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using UniversityVotingSystem.Models;
using UniversityVotingSystem.Repository;
using UniversityVotingSystem.ViewModels;

namespace UniversityVotingSystem.webpages
{
    public class ContactPageModel : PageModel
    {
        public static IActionResult OnGet()
        {
            return new PageResult();
        }
    }
}
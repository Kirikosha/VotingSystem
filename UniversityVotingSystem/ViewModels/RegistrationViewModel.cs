using Microsoft.AspNetCore.Mvc;

namespace UniversityVotingSystem.ViewModels
{
    public class RegistrationViewModel
    {
        [BindProperty]
        public required string email { get; set; }
        [BindProperty]
        public required string password { get; set; }
        [BindProperty]
        public required string first_name { get; set; }
        [BindProperty]
        public required string last_name { get; set; }
        [BindProperty]
        public string? phone_number { get; set; }
    }
}

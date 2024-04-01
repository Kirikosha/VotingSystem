using Microsoft.AspNetCore.Mvc;

namespace UniversityVotingSystem.ViewModels
{
    public class RegistrationViewModel
    {
        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string password { get; set; }
        [BindProperty]
        public string first_name { get; set; }
        [BindProperty]
        public string last_name { get; set; }
        [BindProperty]
        public string phone_number { get; set; }

        private readonly Dictionary<int, string> RegistrationErrorDictionary = new Dictionary<int, string>()
        {
            {1, "Wrong input, email shouldn't begin with @ symbol." },
            {2, "Wrong input, email should't end with @ symbol. " },
            {3, "Password should be longer than 8 characters." },
            {4, "Password shouldn't be longer than 16 characters. " },
            {5, "Your first and last name shouldn't have any digits in them." },
            {6, "Your phone number should start with + symbol." },
            {7, "Your phone number shouldn't be longer than 20 numbers" },
            {8, "Your phone number is written in a wrong way, please make it correct." },
            {9, "All fields have to be filled with info which we need for registration." }
        };

        
    }
}

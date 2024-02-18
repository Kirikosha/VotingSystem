using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UniversityVotingSystem.Models
{
    public class User : IdentityUser
    {
        [Key, Required]
        public string user_id { get; set; }
        [Required]
        public string first_name { get; set; }
        [Required]
        public string second_name { get; set; }
        [Required]
        public string phone_number { get; set; }
        [Required]
        public string role { get; set; }
        public List<UsersVote> Votes { get; set; }
    }
}

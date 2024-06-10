using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityVotingSystem.Models
{
    public class User : IdentityUser
    {
        [Key, Required, Column("user_id")]
        public string? UserId { get; set; }
        [Required, Column("first_name")]
        public string? FirstName { get; set; }
        [Required, Column("second_name")]
        public string? SecondName { get; set; }
        [Required, Column("role")]
        public string Role { get; set; } = "SimpleUser";
        public List<UsersVote> Votes { get; set; } = new List<UsersVote>();
    }
}

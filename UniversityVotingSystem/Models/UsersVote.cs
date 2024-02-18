using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityVotingSystem.Models
{
    public class UsersVote
    {
        [Key, Required]
        public int vote_id {  get; set; }
        [ForeignKey("Proposition"), Required]
        public int proposition_id { get; set; }
        [ForeignKey("User"), Required]
        public string user_id { get; set; }
        public User User { get; set; }
        public Proposition Proposition { get; set; }
    }
}

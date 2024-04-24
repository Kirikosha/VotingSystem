using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityVotingSystem.Models
{
    public class UsersVote
    {
        [Key, Required]
        public required int vote_id {  get; set; }
        [ForeignKey("Proposition"), Required]
        public required int proposition_id { get; set; }
        [ForeignKey("User"), Required]
        public required string user_id { get; set; }
        public required User User { get; set; }
        public required Proposition Proposition { get; set; }
    }
}

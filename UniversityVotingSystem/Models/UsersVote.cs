using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityVotingSystem.Models
{
    public class UsersVote
    {
        [Key, Required, Column("vote_id")]
        public int VoteId {  get; set; }
        [ForeignKey("Proposition"), Required, Column("proposition_id")]
        public required int PropositionId { get; set; }
        [ForeignKey("User"), Required, Column("user_id")]
        public required string UserId { get; set; }
        public  User User { get; set; }
        public  Proposition Proposition { get; set; }
    }
}

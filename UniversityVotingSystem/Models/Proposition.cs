using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityVotingSystem.Models
{
    public class Proposition
    {
        [Key, Required, Column("proposition_id")]
        public int PropositionId {  get; set; }
        [Required, Column("proposition_text")]
        public required string PropositionText { get; set; }
        [ForeignKey("Voting"),Required, Column("voting_id")]
        public int VotingId { get; set; }
        public List<UsersVote> UsersVotes { get; set; } = new List<UsersVote>();
        public Voting? Voting { get; set; }
    }
}

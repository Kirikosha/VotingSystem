using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityVotingSystem.Models
{
    public class Voting
    {
        [Key, Required, Column("voting_id")]
        public int VotingId {  get; set; }
        [Required, Column("voting_name")]
        public required string VotingName { get; set; }
        [Required, Column("state")]
        public required int State {get; set;} // 0 is opened voting | 1 is closed voting
        [Required, Column("created_at")]
        public required DateTime CreatedAt {get; set;}
        [Required, Column("starts_at")]
        public required DateTime StartsAt {get; set;}
        [Required, Column("ends_at")]
        public required DateTime EndsAt {get; set;}
        public List<Proposition> Propositions { get; set; } = new List<Proposition>();
    }
}

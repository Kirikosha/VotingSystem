using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityVotingSystem.Models
{
    public class Proposition
    {
        [Key, Required]
        public int proposition_id {  get; set; }
        [Required]
        public string proposition_text { get; set; }
        [ForeignKey("Voting"),Required]
        public int voting_id { get; set; }
        public List<UsersVote> UsersVotes { get; set; }
        public Voting Voting { get; set; }
    }
}

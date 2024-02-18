using System.ComponentModel.DataAnnotations;

namespace UniversityVotingSystem.Models
{
    public class Voting
    {
        [Key, Required]
        public int voting_id {  get; set; }
        [Required]
        public string voting_name { get; set; }
        public List<Proposition> Propositions { get; set; }
    }
}

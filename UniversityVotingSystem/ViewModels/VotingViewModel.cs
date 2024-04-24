using System.Collections;
using UniversityVotingSystem.Models;

namespace UniversityVotingSystem.ViewModels
{
    public class VotingViewModel
    {
        Voting? voting { get; set; }
        List<Proposition>? propositions { get; set; }

        public VotingViewModel(Voting? voting, List<Proposition>? propositions)
        {
            this.voting = voting;
            this.propositions = propositions;
        }
    }
}

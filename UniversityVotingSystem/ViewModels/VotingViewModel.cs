using System.Collections;
using UniversityVotingSystem.Models;

namespace UniversityVotingSystem.ViewModels
{
    public class VotingViewModel
    {
        Voting voting { get; set; }
        List<Proposition> propositions { get; set; }
        Hashtable propositionPerValue { get; set; }

        public VotingViewModel(Voting voting, List<Proposition> propositions, Hashtable propositionPerValue)
        {
            this.voting = voting;
            this.propositions = propositions;
            this.propositionPerValue = propositionPerValue;
        }
    }
}

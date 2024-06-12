using System.Collections;
using UniversityVotingSystem.Models;

namespace UniversityVotingSystem.ViewModels
{
    public class VotingViewModel
    {
        private Voting? voting;
        private List<Proposition>? propositions;
        private readonly Hashtable hashtable;
        public VotingViewModel(Voting? voting, List<Proposition>? propositions)
        {
            this.voting = voting;
            this.propositions = propositions;
            this.hashtable = new Hashtable();
        }

        public Voting? getVoting()
        {
            return voting;
        }

        public int getPropositionId(Proposition? proposition){
            if(proposition is not null){
                return proposition.PropositionId;
            }
            return -1;
        }

        public Hashtable GetHashTableReference()
        {
            return hashtable;
        }
    }
}

using UniversityVotingSystem.Models;

public sealed class ProposotionsPerVotingObject
        {
            private Voting _voting;
            private List<Proposition> _propositions;
            public ProposotionsPerVotingObject(Voting voting, List<Proposition> propositions)
            {
                _voting = voting;
                _propositions = propositions;
            }

            public Voting GetVoting()
            {
                return _voting;
            }
            public List<Proposition> GetPropositions()
            {
                return _propositions;
            }
        }
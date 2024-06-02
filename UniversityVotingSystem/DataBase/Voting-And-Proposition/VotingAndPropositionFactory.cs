using UniversityVotingSystem.Models;

public class VotingAndProposotionFactory
{
    IPropositionVotingRepository _repository;
    public VotingAndProposotionFactory(IPropositionVotingRepository repository)
    {
        _repository = repository;
    }

        private async Task<(Voting, bool)> CreateVoting(string votingName)
        {
            bool isPresent = await _repository.isPresent(votingName);
            if (isPresent)
            {
                return (new Voting(){voting_name = ""}, true) ;
            }

            Voting voting = new Voting{voting_name = votingName};
            return (voting, false);
        }

        private Proposition[] MapPropositions(string[] propositions)
        {
            Proposition[] propositionsArray = new Proposition[propositions.Length];
            for (int i = 0; i < propositions.Length; i++)
            {
                propositionsArray[i] = new Proposition{proposition_text = propositions[i]};
            }

            return propositionsArray;
        }

        public bool InsertVotingAndPropositions(ParsedJsonResult data)
        {
            (Voting, bool) response = CreateVoting(data.name).Result;
            if (response.Item2 is true)
            {
                return false;
            }

            Voting voting = response.Item1;
            if (data.propositions is null)
            {
                throw new ArgumentNullException(nameof(data.propositions), "Propositions were null on the stage of insertion them into db");
            }
            voting.Propositions = MapPropositions(data.propositions).ToList();
            bool isDbRequestSuccessful = _repository.CreateVoting(voting);
            return isDbRequestSuccessful ? true : false;
        }
}
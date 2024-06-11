using UniversityVotingSystem.Models;

public class VotingAndProposotionFactory
{
    IPropositionVotingRepository _repository;
    public VotingAndProposotionFactory(IPropositionVotingRepository repository)
    {
        _repository = repository;
    }

        private async Task<(Voting?, bool)> CreateVoting(string votingName, int state, DateTime startsAt, DateTime endsAt)
        {
            bool isPresent = await _repository.isPresent(votingName);
            if (isPresent)
            {
                return (null, true) ;
            }
            Voting voting = new Voting{VotingName = votingName, State = state, CreatedAt = DateTime.Now, StartsAt = startsAt, EndsAt = endsAt};
            return (voting, false);
        }

        private Proposition[] MapPropositions(string[] propositions)
        {
            Proposition[] propositionsArray = new Proposition[propositions.Length];
            for (int i = 0; i < propositions.Length; i++)
            {
                propositionsArray[i] = new Proposition{PropositionText = propositions[i]};
            }

            return propositionsArray;
        }

        public bool InsertVotingAndPropositions(ParsedJsonResult data)
        {
            if(data.VotingName is null){
                throw new ArgumentNullException(nameof(data.VotingName));
            }

            (Voting?, bool) response = CreateVoting(data.VotingName, data.State, data.StartsAt, data.EndsAt).Result;
            if (response.Item2 is true)
            {
                return false;
            }

            Voting? voting = response.Item1;
            if (voting is null){
                throw new ArgumentNullException(nameof(voting));
            }

            if (data.Propositions is null)
            {
                throw new ArgumentNullException(nameof(data.Propositions), "Propositions were null on the stage of insertion them into db");
            }

            voting.Propositions = MapPropositions(data.Propositions).ToList();
            bool isDbRequestSuccessful = _repository.CreateVoting(voting);
            return isDbRequestSuccessful;
        }
}
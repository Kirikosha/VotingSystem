using UniversityVotingSystem.Models;

public interface IPropositionVotingRepository
{

        //Proposition dbset methods
        Task<Proposition> GetPropositionById(int PropositionId);
        Task<IEnumerable<UsersVote>> GetVotesForPropositionByPropositionId(int PropositionId);
        Task<bool> AddVoteProposition(Proposition proposition, int voting_id);
        bool UpdateVoteProposition(Proposition proposition);
        bool VoteForProposition(int propositionId);
        bool DeleteProposition(Proposition proposition);


        //Voting dbset methods

        Task<Voting> GetVotingById(int voting_id);
        Task<IEnumerable<Proposition>> GetAllPropositionsById(int voting_id);
        Task<IEnumerable<Voting>> GetAllVotings();
        bool DeleteVoting(Voting voting);
        bool UpdateVoting(Voting voting);
        bool CreateVoting(Voting voting);
        Task<bool> isPresent(string votingName);

}
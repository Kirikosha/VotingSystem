using UniversityVotingSystem.Models;

namespace UniversityVotingSystem.Repository
{
    public interface IDataBaseRepository
    {
        //general methods
        bool SaveChanges();

        //Proposition dbset methods
        Task<Proposition> GetPropositionById(int PropositionId);
        Task<IEnumerable<UsersVote>> GetVotesForPropositionByPropositionId(int PropositionId);
        Task<bool> AddVoteProposition(Proposition proposition, int voting_id);
        bool UpdateVoteProposition(Proposition proposition);
        bool VoteForProposition(int propositionId);
        bool DeleteProposition(Proposition proposition);

        //UsersVote dbset methods
        bool ClearVoteFromProposition(int vote_id);
        Task<User> GetUserByUserVoteId(int vote_id);
        bool ChangeUserVote(UsersVote userVote);


        //Voting dbset methods

        Task<Voting> GetVotingById(int voting_id);
        Task<IEnumerable<Proposition>> GetAllPropositionsById(int voting_id);
        bool DeleteVoting(Voting voting);
        bool UpdateVoting(Voting voting);

        //User dbset methods

        Task<User> GetUserById(string user_id);
        bool UpdateUser(User user);
        Task<IEnumerable<UsersVote>> GetAllUsersVote(string user_id);
        bool DeleteUser(User user);
        bool CreateUser(User user);
    }
}

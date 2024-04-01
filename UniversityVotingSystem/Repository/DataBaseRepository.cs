using Microsoft.EntityFrameworkCore;
using UniversityVotingSystem.DataBase;
using UniversityVotingSystem.Models;

namespace UniversityVotingSystem.Repository
{
    public class DataBaseRepository : IDataBaseRepository
    {
        private ApplicationDBContext _dbContext;
        public DataBaseRepository(ApplicationDBContext context)
        {
            _dbContext = context;
        }
        public async Task<bool> AddVoteProposition(Proposition proposition, int voting_id)
        {
            await _dbContext.AddAsync(proposition);
            return SaveChanges();
        }

        public bool ChangeUserVote(UsersVote userVote)
        {
            throw new NotImplementedException();
        }

        public bool ClearVoteFromProposition(int vote_id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProposition(Proposition proposition)
        {
            _dbContext.Remove(proposition);
            return SaveChanges();
        }

        public bool DeleteUser(User user)
        {
            _dbContext.Remove(user);
            return SaveChanges();
        }

        public bool DeleteVoting(Voting voting)
        {
            _dbContext.Remove(voting);
            return SaveChanges();
        }

        public async Task<IEnumerable<Proposition>> GetAllPropositionsById(int voting_id)
        {
            List<Proposition> proposition = await _dbContext.Proposition.Where(a=>a.voting_id == voting_id).ToListAsync();
            return proposition;
        }

        public async Task<IEnumerable<UsersVote>> GetAllUsersVote(string user_id)
        {
            List<UsersVote> usersVote = await _dbContext.UsersVote.Where(a => a.user_id == user_id).ToListAsync();
            return usersVote;
        }

        public async Task<Proposition> GetPropositionById(int PropositionId)
        {
            Proposition proposition = await _dbContext.Proposition.FirstOrDefaultAsync(a => a.proposition_id == PropositionId);
            return proposition;
        }

        public async Task<User> GetUserById(string user_id)
        {
            User user = await _dbContext.User.FirstOrDefaultAsync(a => a.user_id == user_id);
            return user;
        }

        public async Task<User> GetUserByUserVoteId(int vote_id)
        {
            UsersVote userVote= await GetUsersVoteByVoteId(vote_id);
            User user = await _dbContext.User.FirstOrDefaultAsync(a => a.user_id == userVote.user_id);
            return user;
        }
        private async Task<UsersVote> GetUsersVoteByVoteId(int vote_id)
        {
            return await _dbContext.UsersVote.FirstOrDefaultAsync(a => a.vote_id == vote_id);
        }
        public async Task<IEnumerable<UsersVote>> GetVotesForPropositionByPropositionId(int PropositionId)
        {
            List<UsersVote> usersVoteForProposition = await _dbContext.UsersVote.Where(a=>a.proposition_id == PropositionId).ToListAsync();
            return usersVoteForProposition;
        }

        public async Task<Voting> GetVotingById(int voting_id)
        {
            return await _dbContext.Voting.FirstOrDefaultAsync(a => a.voting_id == voting_id);
        }

        public bool SaveChanges()
        {
            bool result = _dbContext.SaveChanges() > 0 ? true : false;
            return result;
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVoteProposition(Proposition proposition)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVoting(Voting voting)
        {
            throw new NotImplementedException();
        }

        public bool VoteForProposition(int propositionId)
        {
            throw new NotImplementedException();
        }

        public bool CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            return SaveChanges();
        }
    }
}

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

        public async Task<IEnumerable<Proposition>> GetAllPropositionsById(int votingId)
        {
            List<Proposition> proposition = await _dbContext.Proposition.Where(a=>a.VotingId == votingId).ToListAsync();
            return proposition;
        }

        public async Task<IEnumerable<UsersVote>> GetAllUsersVote(string userId)
        {
            List<UsersVote> usersVote = await _dbContext.UsersVote.Where(a => a.UserId == userId).ToListAsync();
            return usersVote;
        }

        public async Task<Proposition> GetPropositionById(int propositionId)
        {
            Proposition? proposition = await _dbContext.Proposition.FirstOrDefaultAsync(a => a.PropositionId == propositionId);
            if (proposition is null)
            {
                throw new ArgumentNullException(nameof(proposition), "Current propositon was not found, that can be a problem in a db. File: DataBaseRepository.cs");
            }

            return proposition;
        }

        public async Task<User> GetUserById(string userId)
        {
            User? user = await _dbContext.User.FirstOrDefaultAsync(a => a.UserId == userId);
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user), "The user was not found. File: DataBaseRepository.cs");
            }

            return user;
        }

        public async Task<User> GetUserByUserVoteId(int voteId)
        {
            UsersVote userVote= await GetUsersVoteByVoteId(voteId);
            User? user = await _dbContext.User.FirstOrDefaultAsync(a => a.UserId == userVote.UserId);
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user), "User was null in GetUserByUserVoteId method in file DataBaseRepository.cs");
            }

            return user;
        }
        private async Task<UsersVote> GetUsersVoteByVoteId(int voteId)
        {
            UsersVote? vote = await _dbContext.UsersVote.FirstOrDefaultAsync(a => a.VoteId == voteId);
            if(vote is null)
            {
                throw new ArgumentNullException(nameof(vote), "Vote was null in GetUsersVoteByVoteId method in file DataBaseRepository.cs");
            }

            return vote;
        }
        public async Task<IEnumerable<UsersVote>> GetVotesForPropositionByPropositionId(int propositionId)
        {
            List<UsersVote> usersVoteForProposition = await _dbContext.UsersVote.Where(a=>a.PropositionId == propositionId).ToListAsync();
            return usersVoteForProposition;
        }

        public async Task<Voting> GetVotingById(int votingId)
        {
            Voting? voting = await _dbContext.Voting.FirstOrDefaultAsync(a => a.VotingId == votingId);
            if (voting is null)
            {
                throw new ArgumentNullException(nameof(voting), "Voting was null in GetVotingById method in file DataBaseRepository.cs");
            }

            return voting;
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

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            List<User> users = await _dbContext.User.Select(a => a).ToListAsync();
            return users;
        }

        public bool CreateVoting(Voting voting)
        {
            _dbContext.Voting.Add(voting);
            return SaveChanges();
        }

        public async Task<bool> isPresent(string votingName)
        {
            Voting? checkedVoting = await _dbContext.Voting.Where(a => a.VotingName.Equals(votingName)).FirstOrDefaultAsync();
            if (checkedVoting is null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Voting>> GetAllVotings()
        {
            IEnumerable<Voting> votings = await _dbContext.Voting.Select(a => a).ToListAsync();
            return votings;
        }

        public int CountVotesByPropositionId(int propositionId)
        {
            int count = _dbContext.UsersVote.Count(a => a.PropositionId == propositionId);
            return count;
        }
    }
}

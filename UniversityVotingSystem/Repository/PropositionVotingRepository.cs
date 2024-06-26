using Microsoft.EntityFrameworkCore;
using UniversityVotingSystem.DataBase;
using UniversityVotingSystem.Models;

public class PropositionVotingRepository : IPropositionVotingRepository
{
    private ApplicationDBContext _dbContext;
    public PropositionVotingRepository(ApplicationDBContext context)
    {
        _dbContext = context;
    }
    public async Task<bool> AddVoteProposition(Proposition proposition, int voting_id)
    {
        await _dbContext.AddAsync(proposition);
        return SaveChanges();
    }

    public bool CreateVoting(Voting voting)
    {
        _dbContext.Voting.Add(voting);
        return SaveChanges();
    }

    public bool DeleteProposition(Proposition proposition)
    {
        _dbContext.Remove(proposition);
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

    public async Task<IEnumerable<Voting>> GetAllVotings()
    {
        IEnumerable<Voting> votings = await _dbContext.Voting.Select(a => a).ToListAsync();
        return votings;
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

    public async Task<bool> isPresent(string votingName)
    {
        Voting? voting = await _dbContext.Voting.FirstOrDefaultAsync(a => a.VotingName == votingName);
        if(voting is null){
            return false;
        }
        return true;
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

    public bool SaveChanges()
    {
        bool result = _dbContext.SaveChanges() > 0 ? true : false;
        return result;
    }
}

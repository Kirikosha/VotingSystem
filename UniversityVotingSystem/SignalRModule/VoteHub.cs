using Microsoft.AspNetCore.SignalR;
using UniversityVotingSystem.Models;
using UniversityVotingSystem.Repository;

namespace UniversityVotingSystem;

public class VoteHub : Hub{
    IDataBaseRepository _repository;
    public VoteHub(IDataBaseRepository repository)
    {
        _repository = repository;
    }
    public async Task Vote(string userId, string propositionIdString, string votingIdString){
        int propositionId = int.Parse(propositionIdString);
        int votingId = int.Parse(votingIdString);
        List<Proposition> propositions = _repository.GetPropositionsByVotingId(votingId).Result.ToList();
        int lastId = CheckIfUservoteInAnyProposition(propositions, userId);
        if(lastId != propositionId){
            UsersVote vote = new UsersVote{UserId = userId, PropositionId = propositionId};
            bool result = _repository.AddUserVote(vote);
            if(result){
                Console.WriteLine("Voted");
                await Clients.All.SendAsync("UpdatedVote", true, propositionId, lastId);
            }
        }
    }

    private int CheckIfUservoteInAnyProposition(List<Proposition> propositions, string userId){
        int foundVoteId = -1;
        int lastPropositionId = -1;
        foreach(var proposition in propositions){
            foundVoteId = _repository.CheckIfUserVotedForProposition(proposition, userId).Result;
            if(foundVoteId != -1){
                lastPropositionId = proposition.PropositionId;
                break;
            }
        }

        if(foundVoteId != -1){
            bool isSuccess = _repository.ClearVoteFromProposition(foundVoteId);
            if(!isSuccess){
                throw new Exception("Something went wrong with the database. Can't delete user vote");
            }
        }
        return lastPropositionId;
    }
}
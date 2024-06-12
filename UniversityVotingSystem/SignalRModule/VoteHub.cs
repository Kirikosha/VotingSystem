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
    public async Task Vote(string userId, string propositionIdString){
        int propositionId = int.Parse(propositionIdString);
        UsersVote vote = new UsersVote{UserId = userId, PropositionId = propositionId};
        bool result = _repository.AddUserVote(vote);
        if(result){
            Console.WriteLine("Voted");
            await Clients.All.SendAsync("UpdatedVote", true, propositionId);
        }
    }
}
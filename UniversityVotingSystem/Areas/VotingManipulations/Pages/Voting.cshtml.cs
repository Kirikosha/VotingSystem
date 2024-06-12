using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityVotingSystem.Models;
using UniversityVotingSystem.Repository;
using UniversityVotingSystem.ViewModels;

namespace UniversityVotingSystem.webpages
{
    public class VotingModel : PageModel
    {
        public VotingViewModel VotingViewModelObj { get; set; }
        public string? UserId{get; set;}
        private UserManager<User> _userManager;
        IDataBaseRepository _repository;
        public VotingModel(IDataBaseRepository repository, UserManager<User> userManager)
        {
            this._repository = repository;
            _userManager = userManager;
            VotingViewModelObj = new VotingViewModel(null, null);
        }

        public async Task<IActionResult> OnGet(int id)
        {
            Voting voting = await _repository.GetVotingById(id);
            if (voting == null)
            {
                return new BadRequestResult();
            }

            List<Proposition> propositions =(List<Proposition>) await _repository.GetAllPropositionsById(id);
            if (!CheckPropositions(propositions))
            {
                return new BadRequestResult();
            }

            VotingViewModelObj = new VotingViewModel(voting, propositions);
            foreach (Proposition proposition in propositions)
            {
                int count = _repository.CountVotesByPropositionId(proposition.PropositionId);
                VotingViewModelObj.GetHashTableReference().updateRow(proposition.PropositionId, new NodeValue{PropositionObj = proposition, Count = count});
            }

            string? Username = HttpContext.User.Identity.Name;
            if(Username is null){
                throw new ArgumentNullException("Username is null", nameof(Username));
            }
            User currentUser = FindUser(Username).Result;
            UserId = currentUser.Id;
            return new PageResult();
        }

        private bool CheckPropositions(List<Proposition> propositions)
        {
            foreach(Proposition proposition in propositions)
            {
                if (proposition == null) return false;
            }
            return true;
        }

        private async Task<int> CountVotes(int propositionId)
        {
            int count = 0;
            List<UsersVote> votes = (List<UsersVote>) await _repository.GetVotesForPropositionByPropositionId(propositionId);
            foreach(UsersVote vote in votes)
            {
                if(CheckVote(vote)) count++;
            }
            return count;
        }

        private bool CheckVote (UsersVote vote)
        {
            if(vote == null) return false;
            return true;
        }

        private async Task<User> FindUser(string username)
        {
            User? user =  await _userManager.FindByNameAsync(username);
            if(user is null)
            {
                throw new ArgumentNullException(nameof(user), "User was not found");
            }

            return user;
        }
    }
}

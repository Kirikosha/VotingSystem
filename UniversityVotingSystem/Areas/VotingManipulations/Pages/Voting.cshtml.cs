using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using UniversityVotingSystem.Models;
using UniversityVotingSystem.Repository;
using UniversityVotingSystem.ViewModels;

namespace UniversityVotingSystem.webpages
{
    public class VotingModel : PageModel
    {
        public VotingViewModel VotingViewModelObj { get; set; }
        IDataBaseRepository _repository;
        public VotingModel(IDataBaseRepository repository)
        {
            this._repository = repository;
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
                int count = _repository.CountVotesByPropositionId(proposition.proposition_id);
                VotingViewModelObj.GetHashTableReference().updateRow(proposition.proposition_id, count);
            }

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
    }
}
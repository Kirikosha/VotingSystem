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
        public VotingViewModel votingVM_ { get; set; }
        IDataBaseRepository _repository;
        public VotingModel(IDataBaseRepository repository)
        {
            _repository = repository;
            votingVM_ = new VotingViewModel(null, null);
        }

        public async Task<IActionResult> OnGet(int id)
        {
            Voting voting = await _repository.GetVotingById(id);
            if (voting == null)
            {
                return new BadRequestResult();
            }

            List<Proposition> propositions =(List<Proposition>) await _repository.GetAllPropositionsById(id);
            if (!checkPropositions(propositions))
            {
                return new BadRequestResult();
            }

            votingVM_ = new VotingViewModel(voting, propositions);
            foreach (Proposition proposition in propositions)
            {
                int count = _repository.CountVotesByPropositionId(proposition.proposition_id);
                votingVM_.GetHashTableReference().updateRow(proposition.proposition_id, count);
            }

            return new PageResult();
        }

        private bool checkPropositions(List<Proposition> propositions)
        {
            foreach(Proposition proposition in propositions)
            {
                if (proposition == null) return false;
            }
            return true;
        }

        private async Task<int> countVotes(int propositionId)
        {
            int count = 0;
            List<UsersVote> votes = (List<UsersVote>) await _repository.GetVotesForPropositionByPropositionId(propositionId);
            foreach(UsersVote vote in votes)
            {
                if(checkVote(vote)) count++;
            }
            return count;
        }

        private bool checkVote (UsersVote vote)
        {
            if(vote == null) return false;
            return true;
        }
    }
}

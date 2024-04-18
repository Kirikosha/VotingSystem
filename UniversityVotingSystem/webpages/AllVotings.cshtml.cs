using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using UniversityVotingSystem.Models;
using UniversityVotingSystem.Repository;
using UniversityVotingSystem.ViewModels;

namespace UniversityVotingSystem.webpages
{
    public class AllVotingsModel : PageModel
    {
        public sealed class ProposotionsPerVotingObject
        {
            private Voting voting;
            private List<Proposition> propositions;
            public ProposotionsPerVotingObject(Voting voting, List<Proposition> propositions)
            {
                this.voting = voting;
                this.propositions = propositions;
            }

            public Voting GetVoting()
            {
                return voting;
            }
            public List<Proposition> GetPropositions()
            {
                return propositions;
            }
        }
        public List<ProposotionsPerVotingObject> propPerVotingObjectList {get; set;}
        private IDataBaseRepository _repository;
        public AllVotingsModel (IDataBaseRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> OnGet()
        {
            List<Voting> votings =_repository.GetAllVotings().Result.ToList();
            Console.WriteLine(votings.Count());
            propPerVotingObjectList = new List<ProposotionsPerVotingObject>(votings.Count());
            
            int index = 0;
            foreach (Voting voting in votings)
            {
                List<Proposition> propositions = _repository.GetAllPropositionsById(voting.voting_id).Result.ToList();
                ProposotionsPerVotingObject propPerVotingObj = new ProposotionsPerVotingObject(voting, propositions);


                propPerVotingObjectList.Add(propPerVotingObj);
                Console.WriteLine(index);
                index++;
            }

            return Page();

        }
    }
}